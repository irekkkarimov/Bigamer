using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchAddCommand;

public class MatchAddCommandHandler : IRequestHandler<MatchAddCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchAddCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchAddCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var gameFromDb = await _dbContext.Games
            .FirstOrDefaultAsync(i => i.Id == props.GameId, cancellationToken);

        if (gameFromDb is null)
            throw new MatchBadRequestException("Game not found");

        var newMatch = new Domain.Entities.Match
        {
            Id = Guid.NewGuid(),
            StartDate = props.StartDate,
            FinishDate = props.FinishDate,
            Game = gameFromDb
        };

        var newMatchInfo = new MatchInfo
        {
            Id = Guid.NewGuid(),
            Match = newMatch,
            Stage = props.Stage,
            Prize = props.Prize,
            Other = ""
        };

        var newMatchLinks = props.Links
            .Select(i => new MatchLink
            {
                Id = Guid.NewGuid(),
                Match = newMatch,
                ServiceName = i.ServiceName,
                Link = i.Link
            })
            .ToList();

        var teamsFromDb = await _dbContext.Teams
            .Where(i => props.Teams.Select(e => e.TeamId).Contains(i.Id))
            .ToListAsync(cancellationToken);

        newMatch.MatchInfo = newMatchInfo;
        newMatch.MatchLinks = newMatchLinks;
        newMatch.Teams = teamsFromDb;
        
        await _dbContext.Matches.AddAsync(newMatch, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}