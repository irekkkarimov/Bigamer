using Bigamer.Application.Common.Exceptions.Link;
using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchAddLinkCommand;

public class MatchAddLinkCommandHandler : IRequestHandler<MatchAddLinkCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchAddLinkCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchAddLinkCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var linkTypeToAdd = LinkType.Undefined;

        if (props.ServiceName is null)
            throw new MatchBadRequestException("Wrong service name");

        foreach (var linkType in (LinkType[])Enum.GetValues(typeof(LinkType)))
        {
            if (linkType.ToString().ToLower().Equals(props.ServiceName.ToLower()))
                linkTypeToAdd = linkType;
        }

        if (linkTypeToAdd is LinkType.Undefined)
            throw new BadLinkServiceException($"Service name {LinkType.Undefined.ToString()} not found");

        if (string.IsNullOrWhiteSpace(props.Link))
            throw new BadLinkException("Wrong link");

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchLinks)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        var doesContainThisLink = matchFromDb.MatchLinks.Any(i => i.ServiceName == linkTypeToAdd);

        if (doesContainThisLink)
            throw new MatchBadRequestException("This type of link already added for match");
        
        var matchLink = new MatchLink
        {
            Id = Guid.NewGuid(),
            Match = matchFromDb,
            ServiceName = linkTypeToAdd,
            Link = props.Link
        };

        await _dbContext.MatchLinks.AddAsync(matchLink, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}