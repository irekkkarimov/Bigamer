using Bigamer.Application.Common.Exceptions.Link;
using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamAddLinkCommand;

public class TeamAddLinkCommandHandler : IRequestHandler<TeamAddLinkCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamAddLinkCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamAddLinkCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var linkTypeToAdd = LinkType.Undefined;

        if (props.ServiceName is null)
            throw new TeamBadRequest("Wrong service name");

        foreach (var linkType in (LinkType[])Enum.GetValues(typeof(LinkType)))
        {
            if (linkType.ToString().ToLower().Equals(props.ServiceName.ToLower()))
                linkTypeToAdd = linkType;
        }

        if (linkTypeToAdd is LinkType.Undefined)
            throw new BadLinkServiceException($"Service name {LinkType.Undefined.ToString()} not found");

        if (string.IsNullOrWhiteSpace(props.Link))
            throw new BadLinkException("Wrong link");

        var teamFromDb = await _dbContext.Teams
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamBadRequest("Team not found");

        var doesContainThisLink = teamFromDb.TeamLinks.Any(i => i.ServiceName == linkTypeToAdd);

        if (doesContainThisLink)
            throw new TeamBadRequest("This type of link already added for match");
        
        var newTeamLink = new TeamLink
        {
            Id = Guid.NewGuid(),
            Team = teamFromDb,
            ServiceName = linkTypeToAdd,
            Link = props.Link
        };

        await _dbContext.TeamLinks.AddAsync(newTeamLink, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}