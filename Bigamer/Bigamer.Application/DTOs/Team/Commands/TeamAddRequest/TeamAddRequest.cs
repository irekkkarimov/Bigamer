namespace Bigamer.Application.DTOs.Team.Commands.TeamAddRequest;

public class TeamAddRequest
{
    public string Name { get; set; } = null!;
    public Guid GameId { get; set; }
}