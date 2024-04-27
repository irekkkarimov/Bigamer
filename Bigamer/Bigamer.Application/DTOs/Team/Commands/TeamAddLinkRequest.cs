using Bigamer.Shared.Enums;

namespace Bigamer.Application.DTOs.Team.Commands;

public class TeamAddLinkRequest
{
    public Guid TeamId { get; set; }
    public LinkType ServiceName { get; set; }
    public string Link { get; set; } = null!;
}