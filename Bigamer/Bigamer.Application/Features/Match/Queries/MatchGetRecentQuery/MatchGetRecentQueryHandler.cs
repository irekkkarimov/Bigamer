using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Queries.MatchGetRecentQuery;

public class MatchGetRecentQueryHandler : IRequestHandler<MatchGetRecentQuery, MatchGetRecentResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchGetRecentQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchGetRecentResponse> Handle(MatchGetRecentQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (props.MatchCount <= 0)
            return new MatchGetRecentResponse();

        var matchesFromDb = await _dbContext.Matches
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchTeams)
            .ThenInclude(i => i.Team)
            .Include(i => i.Game)
            .Include(i => i.Teams)
            .OrderByDescending(i => i.StartDate)
            .Take(props.MatchCount)
            .ToListAsync(cancellationToken);

        return new MatchGetRecentResponse
        {
            Matches = matchesFromDb
                .Select(i => new MatchGetRecentResponseItem
                {
                    MatchId = i.Id,
                    GameName = i.Game.Name,
                    StartDate = i.StartDate,
                    FinishDate = i.FinishDate,
                    Prize = i.MatchInfo.Prize,
                    Teams = i.MatchTeams
                        .Select(e => new MatchGetRecentResponseTeam
                        {
                            TeamId = e.TeamId,
                            TeamName = e.Team.Name,
                            TakenPlace = e.TakenPlace
                        })
                        .ToList()
                })
                .ToList()
        };
    }
}