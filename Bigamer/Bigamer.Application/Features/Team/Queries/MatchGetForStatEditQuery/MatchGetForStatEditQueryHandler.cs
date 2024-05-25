using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Team.Queries.MatchGetForStatEditRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Queries.MatchGetForStatEditQuery;

public class MatchGetForStatEditQueryHandler : IRequestHandler<MatchGetForStatEditQuery, MatchGetForStatEditResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchGetForStatEditQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MatchGetForStatEditResponse> Handle(MatchGetForStatEditQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchTeams)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        var teamStats = matchFromDb.MatchTeams
            .FirstOrDefault(i => i.TeamId == props.TeamId);

        if (teamStats is null)
            throw new MatchBadRequestException("Team not found");

        return new MatchGetForStatEditResponse
        {
            TeamId = teamStats.TeamId,
            MatchId = teamStats.MatchId,
            Score = teamStats.TeamResult,
            TakenPlace = teamStats.TakenPlace
        };
    }
}