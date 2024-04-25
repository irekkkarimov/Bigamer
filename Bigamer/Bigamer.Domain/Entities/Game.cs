using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class Game : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Team> Teams { get; set; } = new();
    public List<Match> Matches { get; set; } = new();
}