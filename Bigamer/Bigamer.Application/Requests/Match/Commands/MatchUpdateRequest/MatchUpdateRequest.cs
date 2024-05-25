namespace Bigamer.Application.Requests.Match.Commands.MatchUpdateRequest;

public class MatchUpdateRequest
{
    public Guid MatchId { get; set; }
    public double? Prize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}