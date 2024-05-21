using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Queries.MatchGetAllForHomeQuery;

public class MatchGetAllForHomeQueryHandler : IRequestHandler<MatchGetAllForHomeQuery, MatchGetAllResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchGetAllForHomeQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchGetAllResponse> Handle(MatchGetAllForHomeQuery request, CancellationToken cancellationToken)
    {
        var matchesQueryable = _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .Include(i => i.Teams)
            .ThenInclude(i => i.MatchTeams)
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchLinks);

        var currentMatches = (await matchesQueryable
            .Where(i => i.StartDate < DateTime.UtcNow
                        && (i.FinishDate == null || i.FinishDate > DateTime.UtcNow))
            .OrderByDescending(i => i.StartDate)
            .Take(3)
            .ToListAsync(cancellationToken: cancellationToken))
            .Select(i => _mapper.Map<MatchGetAllResponseItem>(i))
            .ToList();
        
        var upcomingMatches = (await matchesQueryable
            .Where(i => i.StartDate > DateTime.UtcNow)
            .OrderByDescending(i => i.StartDate)
            .Take(3)
            .ToListAsync(cancellationToken: cancellationToken))
            .Select(i => _mapper.Map<MatchGetAllResponseItem>(i))
            .ToList();
        
        var previousMatches = (await matchesQueryable
            .Where(i => i.FinishDate < DateTime.UtcNow)
            .OrderByDescending(i => i.StartDate)
            .Take(3)
            .ToListAsync(cancellationToken: cancellationToken))
            .Select(i => _mapper.Map<MatchGetAllResponseItem>(i))
            .ToList();

        return new MatchGetAllResponse
        {
            CurrentMatches = currentMatches,
            UpcomingMatches = upcomingMatches,
            PreviousMatches = previousMatches,
            Filter = MatchListFilter.All
        };
    }
}