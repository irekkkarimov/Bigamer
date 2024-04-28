using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Match.Commands.MatchAddRequest;

public class MatchAddRequestLink
{
    public LinkType ServiceName { get; set; }
    public string Link { get; set; } = null!;
}