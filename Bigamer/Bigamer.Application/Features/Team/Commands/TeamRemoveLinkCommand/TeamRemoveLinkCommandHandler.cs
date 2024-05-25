using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamRemoveLinkCommand;

public class TeamRemoveLinkCommandHandler : IRequestHandler<TeamRemoveLinkCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamRemoveLinkCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamRemoveLinkCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamFromDb = await _dbContext.Teams
            .Include(i => i.TeamLinks)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new BadTeamGameException("Team not found");

        var linkToDelete = teamFromDb.TeamLinks
            .FirstOrDefault(i => i.ServiceName.ToString().Equals(props.LinkType));


        if (linkToDelete is null)
            throw new TeamBadRequest("Team link to remove to found");
        
        _dbContext.TeamLinks.Remove(linkToDelete);

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}