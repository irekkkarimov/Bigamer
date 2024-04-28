using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllResponse
{
    public List<MatchGetAllResponseItem> Matches { get; set; } = new();
    public MatchListFilter Filter { get; set; }
}