namespace Bigamer.Application.Requests.Team.Commands.TeamAddPlayerRequest;

public class TeamAddPlayerRequest
{
    public Guid TeamId { get; set; }
    public Guid PlayerId { get; set; }
}