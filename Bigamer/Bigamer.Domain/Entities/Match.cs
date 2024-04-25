using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class Match : BaseEntity
{
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public Guid GameId { get; set; }
    public Game Game { get; set; } = null!;
    public List<Team> Teams { get; set; } = new();
    public List<MatchTeam> MatchTeams { get; set; } = new();
    public MatchInfo MatchInfo { get; set; } = null!;
    public List<MatchLink> MatchLinks { get; set; } = new();
}