using AutoMapper;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;

public class TeamGetAllQueryHandler : IRequestHandler<TeamGetAllQuery, TeamGetAllResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public TeamGetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TeamGetAllResponse> Handle(TeamGetAllQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamsFromDb = _dbContext.Teams
            .Include(i => i.TeamInfo)
            .Include(i => i.TeamLinks)
            .Include(i => i.Game);

        var totalCount = await teamsFromDb.CountAsync(cancellationToken);
        
        var teamsFromDbToList = await teamsFromDb
            .Skip(props.Offset)
            .Take(props.Limit)
            .ToListAsync(cancellationToken);
        
        var result = new TeamGetAllResponse
        {
            CurrentOffset = props.Offset,
            CurrentLimit = props.Limit,
            TotalCount = totalCount
        };
        
        foreach (var team in teamsFromDbToList)
            result.Teams.Add(_mapper.Map<TeamGetAllResponseItem>(team));

        return result;
    }
}