namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllResponseTeam
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public List<MatchGetAllResponseTeamMatch> TeamMatches { get; set; } = new();
    public int TakenPlace { get; set; }
    public int TeamResult { get; set; }
}