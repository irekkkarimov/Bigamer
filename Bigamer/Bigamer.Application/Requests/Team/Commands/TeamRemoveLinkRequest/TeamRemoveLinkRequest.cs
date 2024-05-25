using Bigamer.Shared.Enums;

namespace Bigamer.Application.Requests.Team.Commands.TeamRemoveLinkRequest;

public class TeamRemoveLinkRequest
{
    public Guid TeamId { get; set; }
    public string LinkType { get; set; } = null!;
}