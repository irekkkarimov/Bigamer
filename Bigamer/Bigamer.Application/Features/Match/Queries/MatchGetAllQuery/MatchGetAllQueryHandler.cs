using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;

public class MatchGetAllQueryHandler : IRequestHandler<MatchGetAllQuery, MatchGetAllResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchGetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchGetAllResponse> Handle(MatchGetAllQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        var filter = MatchListFilter.All;

        if (props.Filter is not null)
        {
            foreach (var filterType in (MatchListFilter[])Enum.GetValues(typeof(MatchListFilter)))
            {
                if (filterType.ToString().ToLower().Equals(props.Filter.ToLower()))
                    filter = filterType;
            }
        }

        var allMatches = _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.MatchTeams)
            .ThenInclude(i => i.Team)
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .Include(i => i.Teams)
            .ThenInclude(i => i.MatchTeams)
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchLinks);
        
        IQueryable<Domain.Entities.Match> allMatchesOrdered;

        switch (filter)
        {
            default:
            case MatchListFilter.All:
            {
                allMatchesOrdered = allMatches;
                break;
            }
            case MatchListFilter.Today:
            {
                allMatchesOrdered = allMatches
                    .Where(i =>
                        i.StartDate != null && DateTime.Now.Day == i.StartDate.Value.Day
                                            && DateTime.Now.Month == i.StartDate.Value.Month
                                            && DateTime.Now.Year == i.StartDate.Value.Year);
                break;
            }
            case MatchListFilter.Upcoming:
            {
                allMatchesOrdered = allMatches
                    .Where(i =>
                        i.StartDate != null && i.StartDate.Value.ToLocalTime() > DateTime.Now.ToLocalTime());
                break;
            }
            case MatchListFilter.Current:
            {
                allMatchesOrdered = allMatches
                    .Where(i =>
                        i.StartDate != null && i.StartDate.Value.ToLocalTime() < DateTime.Now.ToLocalTime()
                        && i.FinishDate == null);
                break;
            }
            case MatchListFilter.Results:
            {
                allMatchesOrdered = allMatches
                    .Where(i =>
                        i.StartDate != null && i.StartDate.Value.ToLocalTime() < DateTime.Now.ToLocalTime()
                        && i.FinishDate != null && i.FinishDate.Value.ToLocalTime() < DateTime.Now.ToLocalTime());
                break;
            }
        }
        
        var totalCount = await allMatchesOrdered.CountAsync(cancellationToken);

        return new MatchGetAllResponse
        {
            MatchesByFilter = await allMatchesOrdered
                .OrderByDescending(i => i.StartDate)
                .Skip(props.Offset)
                .Take(props.Limit)
                .Select(i => new MatchGetAllResponseItem
                {
                    MatchId = i.Id,
                    GameName = i.Game.Name,
                    StartDate = i.StartDate,
                    FinishDate = i.FinishDate,
                    Prize = i.MatchInfo.Prize,
                    Teams = i.MatchTeams
                        .Select(e => new MatchGetAllResponseTeam
                        {
                            TeamId = e.TeamId,
                            TeamName = e.Team.Name,
                            ImageUrl = e.Team.TeamInfo.ImageUrl,
                            TakenPlace = e.TakenPlace,
                            TeamResult = e.TeamResult
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken),
            Filter = filter,
            CurrentOffset = props.Offset,
            CurrentLimit = props.Limit,
            TotalCount = totalCount
        };
    }
}