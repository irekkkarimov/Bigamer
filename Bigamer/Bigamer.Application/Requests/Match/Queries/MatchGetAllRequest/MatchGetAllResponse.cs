namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllResponse
{
    public List<MatchGetAllResponseItem> Matches { get; set; } = new();
}