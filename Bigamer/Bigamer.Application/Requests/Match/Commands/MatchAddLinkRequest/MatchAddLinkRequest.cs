namespace Bigamer.Application.Requests.Match.Commands.MatchAddLinkRequest;

public class MatchAddLinkRequest
{
    public Guid MatchId { get; set; }
    public string ServiceName { get; set; } = null!;
    public string Link { get; set; } = null!;
}