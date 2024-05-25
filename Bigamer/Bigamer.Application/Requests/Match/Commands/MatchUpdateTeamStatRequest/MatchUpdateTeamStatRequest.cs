namespace Bigamer.Application.Requests.Match.Commands.MatchUpdateTeamStatRequest;

public class MatchUpdateTeamStatRequest
{
    public Guid MatchId { get; set; }
    public Guid TeamId { get; set; }
    public int Score { get; set; }
}