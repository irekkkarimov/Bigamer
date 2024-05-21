using Bigamer.Application.Requests.Admin.Queries.GetDashboardStatisticsRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;

namespace Bigamer.Application.DTOs.Admin;

public class AdminDashboardModel
{
    public GetDashboardStatisticsResponse Statistics { get; set; } = null!;
    public MatchGetRecentResponse RecentMatches { get; set; } = null!;
}