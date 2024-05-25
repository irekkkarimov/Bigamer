using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamUpdateCommand;

public class TeamUpdateCommandHandler : IRequestHandler<TeamUpdateCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamUpdateCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamUpdateCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamBadRequest("Team not found");

        if (!string.IsNullOrWhiteSpace(props.TeamName))
            teamFromDb.Name = props.TeamName;

        if (!string.IsNullOrWhiteSpace(props.TeamName))
            teamFromDb.TeamInfo.ImageUrl = props.Image;
        
        teamFromDb.Name = props.TeamName;
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}