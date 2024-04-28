using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
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
        var allMatches = await _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.Teams)
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchLinks)
            .ToListAsync(cancellationToken);

        return new MatchGetAllResponse
        {
            Matches = allMatches.Select(i => _mapper.Map<MatchGetAllResponseItem>(i)).ToList()
        };
    }
}