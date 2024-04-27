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

        if (props.ServiceName is LinkType.Undefined)
            throw new BadLinkServiceException($"Service name {LinkType.Undefined.ToString()} not found");

        if (string.IsNullOrWhiteSpace(props.Link))
            throw new BadLinkException("Wrong link");

        var teamFromDb = await _dbContext.Teams
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamBadRequest("Team not found");

        var newTeamLink = new TeamLink
        {
            Id = Guid.NewGuid(),
            Team = teamFromDb,
            ServiceName = props.ServiceName,
            Link = props.Link
        };

        await _dbContext.TeamLinks.AddAsync(newTeamLink, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}