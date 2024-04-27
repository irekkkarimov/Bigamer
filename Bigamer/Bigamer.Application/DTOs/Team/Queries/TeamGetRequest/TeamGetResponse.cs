using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bigamer.Application.DTOs.Team.Queries.TeamGetRequest;

public class TeamGetResponse
{
    public string Name { get; set; } = null!;
    public int WinsCount { get; set; }
    public int LosesCount { get; set; }
    public int DrawsCount { get; set; }
    public int GamesCount { get; set; }
    public string? ImageUrl { get; set; }
    public string? About { get; set; }
    public TeamGetResponseGame Game { get; set; } = null!;
    public List<TeamGetResponsePlayer> Players { get; set; } = new();
}