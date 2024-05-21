using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Admin.Queries.GetDashboardStatisticsRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Admin.Queries.GetDashboardStatisticsQuery;

public class GetDashboardStatisticsQueryHandler 
    : IRequestHandler<GetDashboardStatisticsQuery, GetDashboardStatisticsResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetDashboardStatisticsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetDashboardStatisticsResponse> Handle(GetDashboardStatisticsQuery request, CancellationToken cancellationToken)
    {
        var allUserCount = await _dbContext.Users
            .CountAsync(cancellationToken);

        var allTeamCount = await _dbContext.Teams
            .CountAsync(cancellationToken);

        var allMatchesCount = await _dbContext.Matches
            .CountAsync(cancellationToken);

        return new GetDashboardStatisticsResponse
        {
            TotalUsers = allUserCount,
            TotalTeams = allTeamCount,
            TotalMatches = allMatchesCount
        };
    }
}