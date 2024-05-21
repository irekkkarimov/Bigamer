namespace Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;

public class MatchGetRecentResponse
{
    public List<MatchGetRecentResponseItem> Matches { get; set; } = new();
}