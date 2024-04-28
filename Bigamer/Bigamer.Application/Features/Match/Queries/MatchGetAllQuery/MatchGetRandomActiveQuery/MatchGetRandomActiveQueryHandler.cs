using System.Text.Json;
using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetRandomActiveRequest;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Queries.MatchGetAllQuery.MatchGetRandomActiveQuery;

public class MatchGetRandomActiveQueryHandler : IRequestHandler<MatchGetRandomActiveQuery, MatchGetRandomActiveResponse?>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public MatchGetRandomActiveQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<MatchGetRandomActiveResponse?> Handle(MatchGetRandomActiveQuery request, CancellationToken cancellationToken)
    {
        var match = await _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.MatchInfo)
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .Include(i => i.MatchLinks)
            .Where(i => i.FinishDate == null)
            .OrderBy(i => Guid.NewGuid())
            .FirstOrDefaultAsync(cancellationToken);
        
        if (match is null)
            return null;

        var watchLink = "";
        var hltvLink = match.MatchLinks.FirstOrDefault(i => i.ServiceName == LinkType.Hltv);

        if (hltvLink is not null)
            watchLink = hltvLink.Link;
        else
        {
            var randomLink = match.MatchLinks.FirstOrDefault();
            if (randomLink is not null)
                watchLink = randomLink.Link;
        }

        var result = new MatchGetRandomActiveResponse
        {
            GameName = match.Game.Name,
            StartDate = match.StartDate,
            WatchLink = watchLink,
            Teams = match.Teams.Select(i => _mapper.Map<MatchGetAllResponseTeam>(i)).ToList()
        };

        return result;
    }
}