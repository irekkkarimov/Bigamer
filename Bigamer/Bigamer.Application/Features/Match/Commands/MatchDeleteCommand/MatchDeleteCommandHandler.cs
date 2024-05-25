using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchDeleteCommand;

public class MatchDeleteCommandHandler : IRequestHandler<MatchDeleteCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchDeleteCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchDeleteCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        _dbContext.Matches.Remove(matchFromDb);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}