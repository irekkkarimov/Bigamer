using Bigamer.Domain.Common;
using Bigamer.Shared.Enums;

namespace Bigamer.Domain.Entities;

public class MatchLink : BaseEntity
{
    public Guid MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public MatchLinkType ServiceName { get; set; } = MatchLinkType.Undefined;
    public string Link { get; set; } = null!;
}