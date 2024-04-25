using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class MatchTeam
{
    public Guid MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = null!;
    public int TeamResult { get; set; }
    public int TakenPlace { get; set; }
}