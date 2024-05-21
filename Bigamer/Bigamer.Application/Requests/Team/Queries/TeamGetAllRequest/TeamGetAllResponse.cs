namespace Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;

public class TeamGetAllResponse
{
    public List<TeamGetAllResponseItem> Teams { get; set; } = new();
    public int CurrentOffset { get; set; }
    public int CurrentLimit { get; set; }
    public int TotalCount { get; set; }
}