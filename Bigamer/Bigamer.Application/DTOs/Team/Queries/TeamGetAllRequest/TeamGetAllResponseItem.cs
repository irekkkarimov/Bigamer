namespace Bigamer.Application.DTOs.Team.Queries.TeamGetAllRequest;

public class TeamGetAllResponseItem
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
}