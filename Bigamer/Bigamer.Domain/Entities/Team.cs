using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid GameId { get; set; }
    public Game Game { get; set; } = null!;
    public TeamInfo TeamInfo { get; set; } = null!;
    public List<Match> Matches { get; set; } = new();
    public List<MatchTeam> MatchTeams { get; set; } = new();
    public List<UserInfo> UserInfos { get; set; } = new();
    public List<TeamLink> TeamLinks { get; set; } = new();
}