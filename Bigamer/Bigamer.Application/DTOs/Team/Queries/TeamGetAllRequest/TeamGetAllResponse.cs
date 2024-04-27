namespace Bigamer.Application.DTOs.Team.Queries.TeamGetAllRequest;

public class TeamGetAllResponse
{
    public List<TeamGetAllResponseItem> Teams { get; set; } = new();
}