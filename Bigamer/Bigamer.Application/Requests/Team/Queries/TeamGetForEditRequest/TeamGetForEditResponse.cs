using Bigamer.Application.DTOs.Links;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

namespace Bigamer.Application.Requests.Team.Queries.TeamGetForEditRequest;

public class TeamGetForEditResponse
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public string GameName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public List<GetLink> Links { get; set; } = new();
    public List<UserGetAllResponseItem> Players { get; set; } = new();
}