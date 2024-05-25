namespace Bigamer.Application.Requests.Match.Commands.MatchRemoveLinkRequest;

public class MatchRemoveLinkRequest
{
    public Guid MatchId { get; set; }
    public string ServiceName { get; set; } = null!;
}