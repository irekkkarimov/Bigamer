using Bigamer.Domain.Common;
using Bigamer.Shared.Enums;

namespace Bigamer.Domain.Entities;

public class MatchLink : BaseEntity
{
    public Guid MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public LinkType ServiceName { get; set; } = LinkType.Undefined;
    public string Link { get; set; } = null!;
}