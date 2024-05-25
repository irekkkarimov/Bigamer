namespace Bigamer.Application.Requests.Team.Commands.TeamAddRequest;

public class TeamAddRequest
{
    public string Name { get; set; } = null!;
    public Guid GameId { get; set; }
    public string? Image { get; set; }
}