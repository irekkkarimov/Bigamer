using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchRemoveLinkCommand;

public class MatchRemoveLinkCommandHandler : IRequestHandler<MatchRemoveLinkCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchRemoveLinkCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchRemoveLinkCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (string.IsNullOrWhiteSpace(props.ServiceName))
            throw new MatchBadRequestException("Wrong service name");

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchLinks)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");
        
        var linkToDelete = matchFromDb.MatchLinks
            .FirstOrDefault(i => i.ServiceName.ToString().Equals(props.ServiceName));

        if (linkToDelete is not null)
            _dbContext.MatchLinks.Remove(linkToDelete);

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}