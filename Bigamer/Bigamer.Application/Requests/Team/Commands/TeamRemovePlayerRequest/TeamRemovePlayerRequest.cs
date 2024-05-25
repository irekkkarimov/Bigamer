namespace Bigamer.Application.Requests.Team.Commands.TeamRemovePlayerRequest;

public class TeamRemovePlayerRequest
{
    public Guid TeamId { get; set; }
    public Guid PlayerId { get; set; }
}