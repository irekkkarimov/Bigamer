using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

namespace Bigamer.Application.Requests.Match.Queries.MatchGetRandomActiveRequest;

public class MatchGetRandomActiveResponse
{
    public string GameName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public string? WatchLink { get; set; }
    public List<MatchGetAllResponseTeam> Teams { get; set; } = new();
}