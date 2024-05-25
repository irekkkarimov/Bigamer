namespace Bigamer.Application.Requests.Team.Commands.TeamUpdateRequest;

public class TeamUpdateRequest
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public string? Image { get; set; }
}