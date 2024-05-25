using AutoMapper;
using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.DTOs.Links;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetForEditRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Queries.MatchGetForEditQuery;

public class MatchGetForEditQueryHandler : IRequestHandler<MatchGetForEditQuery, MatchGetForEditResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchGetForEditQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchGetForEditResponse> Handle(MatchGetForEditQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.MatchTeams)
            .ThenInclude(i => i.Team)
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .Include(i => i.Teams)
            .ThenInclude(i => i.MatchTeams)
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchLinks)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchNotFoundException("Match with this id not found");

        return new MatchGetForEditResponse
        {
            MatchId = matchFromDb.Id,
            GameName = matchFromDb.Game.Name,
            StartDate = matchFromDb.StartDate?.ToLocalTime(),
            FinishDate = matchFromDb.FinishDate?.ToLocalTime(),
            Prize = matchFromDb.MatchInfo.Prize,
            Teams = matchFromDb.MatchTeams
                .Select(e => new MatchGetAllResponseTeam
                {
                    TeamId = e.TeamId,
                    TeamName = e.Team.Name,
                    ImageUrl = e.Team.TeamInfo.ImageUrl,
                    TakenPlace = e.TakenPlace,
                    TeamResult = e.TeamResult
                })
                .ToList(),
            Links = matchFromDb.MatchLinks
                .Select(i => _mapper.Map<GetLink>(i))
                .ToList()
        };
    }
}