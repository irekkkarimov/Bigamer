namespace Bigamer.Application.Requests.Team.Queries.MatchGetForStatEditRequest;

public class MatchGetForStatEditResponse
{
    public Guid TeamId { get; set; }
    public Guid MatchId { get; set; }
    public int Score { get; set; }
    public int TakenPlace { get; set; }
}