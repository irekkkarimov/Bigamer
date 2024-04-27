using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamAddCommand;

public class TeamAddCommandHandler : IRequestHandler<TeamAddCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamAddCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamAddCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (string.IsNullOrWhiteSpace(props.Name))
            throw new TeamValidationException("Wrong team name");

        var gameFromDb = await _dbContext.Games
            .FirstOrDefaultAsync(i =>
                i.Id.Equals(props.GameId), cancellationToken: cancellationToken);

        if (gameFromDb is null)
            throw new BadTeamGameException("Game not found");

        var newTeam = new Domain.Entities.Team
        {
            Id = Guid.NewGuid(),
            Name = props.Name,
            Game = gameFromDb
        };

        var newTeamInfo = new TeamInfo
        {
            Id = Guid.NewGuid(),
            Team = newTeam,
            WinsCount = 0,
            LosesCount = 0,
            DrawsCount = 0,
            GamesCount = 0
        };

        await _dbContext.Teams.AddAsync(newTeam, cancellationToken);
        await _dbContext.TeamInfos.AddAsync(newTeamInfo, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}