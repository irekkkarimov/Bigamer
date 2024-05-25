namespace Bigamer.Application.Requests.Match.Commands.MatchRemoveTeamRequest;

public class MatchRemoveTeamRequest
{
    public Guid MatchId { get; set; }
    public Guid TeamId { get; set; }
}