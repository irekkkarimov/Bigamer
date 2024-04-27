using Bigamer.Shared.Enums;

namespace Bigamer.Application.DTOs.Team.Queries.TeamGetRequest;

public class TeamGetLink
{
    public LinkType ServiceName { get; set; }
    public string Link { get; set; } = null!;
}