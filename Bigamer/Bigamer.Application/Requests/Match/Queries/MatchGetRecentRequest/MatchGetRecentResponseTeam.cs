namespace Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;

public class MatchGetRecentResponseTeam
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public int TakenPlace { get; set; }
}