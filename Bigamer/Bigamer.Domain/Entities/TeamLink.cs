using Bigamer.Domain.Common;
using Bigamer.Shared.Enums;

namespace Bigamer.Domain.Entities;

public class TeamLink : BaseEntity
{
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = null!;
    public LinkType ServiceName { get; set; } = LinkType.Undefined;
    public string Link { get; set; } = null!;
}