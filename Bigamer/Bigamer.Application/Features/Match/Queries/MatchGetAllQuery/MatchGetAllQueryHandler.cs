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
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchLinks);

        var allMatchesToList = new List<Domain.Entities.Match>();

        switch (filter)
        {
            default:
            case MatchListFilter.All:
            {
                allMatchesToList = await allMatches.ToListAsync(cancellationToken);
                break;
            }
            case MatchListFilter.Today:
            {
                allMatchesToList = await allMatches
                    .Where(i =>
                        i.StartDate != null && DateTime.Now.Day == i.StartDate.Value.Day
                                            && DateTime.Now.Month == i.StartDate.Value.Month
                                            && DateTime.Now.Year == i.StartDate.Value.Year)
                    .ToListAsync(cancellationToken);
                break;
            }
            case MatchListFilter.Upcoming:
            {
                allMatchesToList = await allMatches
                    .Where(i =>
                        i.StartDate != null && i.StartDate.Value.ToLocalTime() > DateTime.Now.ToLocalTime())
                    .ToListAsync(cancellationToken);
                break;
            }
            case MatchListFilter.Results:
            {
                allMatchesToList = await allMatches
                    .Where(i =>
                        i.StartDate != null && i.StartDate.Value.ToLocalTime() < DateTime.Now.ToLocalTime())
                    .ToListAsync(cancellationToken);
                break;
            }
        }

        return new MatchGetAllResponse
        {
            Matches = allMatchesToList.Select(i => _mapper.Map<MatchGetAllResponseItem>(i)).ToList(),
            Filter = filter
        };
    }
}