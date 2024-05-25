namespace Bigamer.Application.Requests.Match.Commands.MatchAddTeamRequest;

public class MatchAddTeamRequest
{
    public Guid MatchId { get; set; }
    public Guid TeamId { get; set; }
}