namespace Bigamer.Application.Requests.Admin.Queries.GetDashboardStatisticsRequest;

public class GetDashboardStatisticsResponse
{
    public int TotalUsers { get; set; }
    public int TotalTeams { get; set; }
    public int TotalMatches { get; set; }
}