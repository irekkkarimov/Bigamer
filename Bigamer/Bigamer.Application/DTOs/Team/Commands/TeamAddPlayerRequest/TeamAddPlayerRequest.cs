namespace Bigamer.Application.DTOs.Team.Commands.TeamAddPlayerRequest;

public class TeamAddPlayerRequest
{
    public Guid TeamId { get; set; }
    public Guid PlayerId { get; set; }
}