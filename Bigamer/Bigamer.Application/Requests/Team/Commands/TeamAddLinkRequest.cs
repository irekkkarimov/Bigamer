using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Team.Commands;

public class TeamAddLinkRequest
{
    public Guid TeamId { get; set; }
    public string ServiceName { get; set; } = null!;
    public string Link { get; set; } = null!;
}