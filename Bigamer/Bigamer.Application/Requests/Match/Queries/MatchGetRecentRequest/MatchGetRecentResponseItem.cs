using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;

public class MatchGetRecentResponseItem
{
    public Guid MatchId { get; set; }
    public string GameName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public double? Prize { get; set; }
    public List<MatchGetRecentResponseTeam> Teams { get; set; } = new();
}