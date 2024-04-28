using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class MatchInfo : BaseEntity
{
    public Guid MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public double? Stage { get; set; }
    public double? Prize { get; set; }
    public string? Other { get; set; }
}