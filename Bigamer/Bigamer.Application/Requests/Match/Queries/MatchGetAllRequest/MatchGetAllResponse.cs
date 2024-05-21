using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllResponse
{
    public List<MatchGetAllResponseItem> MatchesByFilter { get; set; } = new();
    public List<MatchGetAllResponseItem> CurrentMatches { get; set; } = new();
    public List<MatchGetAllResponseItem> UpcomingMatches { get; set; } = new();
    public List<MatchGetAllResponseItem> PreviousMatches { get; set; } = new();
    public MatchListFilter Filter { get; set; }
    public int CurrentOffset { get; set; }
    public int CurrentLimit { get; set; }
    public int TotalCount { get; set; }
}