namespace Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;

public class MatchGetAllRequest
{
    public string? Filter { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}