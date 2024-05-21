using Bigamer.Application.DTOs.Links;

namespace Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;

public class TeamGetAllResponseItem
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = null!;
    public string GameName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public List<GetLink> Links { get; set; } = new();
}