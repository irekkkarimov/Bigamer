using Bigamer.Domain.Common;

namespace Bigamer.Domain.Entities;

public class TeamInfo : BaseEntity
{
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = null!;
    public int WinsCount { get; set; }
    public int LosesCount { get; set; }
    public int DrawsCount { get; set; }
    public int GamesCount { get; set; }
    public string? ImageUrl { get; set; }
}