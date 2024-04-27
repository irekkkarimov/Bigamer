using AutoMapper;
using Bigamer.Application.DTOs.Team.Queries.TeamGetAllRequest;
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
        var teamsFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .ToListAsync(cancellationToken);

        var result = new TeamGetAllResponse();
        
        foreach (var team in teamsFromDb)
            result.Teams.Add(_mapper.Map<TeamGetAllResponseItem>(team));

        return result;
    }
}