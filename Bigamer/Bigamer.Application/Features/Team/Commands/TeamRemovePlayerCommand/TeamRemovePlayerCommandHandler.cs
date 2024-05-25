using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamRemovePlayerCommand;

public class TeamRemovePlayerCommandHandler : IRequestHandler<TeamRemovePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamRemovePlayerCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamRemovePlayerCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        
        var teamFromDb = await _dbContext.Teams
            .Include(i => i.UserInfos)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new BadTeamGameException("Team not found");

        var userToRemove = teamFromDb.UserInfos
            .FirstOrDefault(i => i.UserId == props.PlayerId);

        if (userToRemove is null)
            throw new TeamBadRequest("User to remove not found");

        teamFromDb.UserInfos.Remove(userToRemove);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}