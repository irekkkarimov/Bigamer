using AutoMapper;
using Bigamer.Application.Requests.Team.Queries.TeamGetRequest;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Queries.TeamGetQuery;

public class TeamGetQueryHandler : IRequestHandler<TeamGetQuery, TeamGetResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public TeamGetQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TeamGetResponse> Handle(TeamGetQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .Include(i => i.UserInfos)
            .Include(i => i.Game)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        return _mapper.Map<TeamGetResponse>(teamFromDb);
    }
}