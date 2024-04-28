namespace Bigamer.Application.Requests.Match.Commands.MatchAddRequest;

public class MatchAddRequest
{
    public Guid GameId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public double? Stage { get; set; }
    public double? Prize { get; set; }
    public List<MatchAddRequestLink> Links { get; set; } = new();
    public List<MatchAddRequestTeam> Teams { get; set; } = new();
}