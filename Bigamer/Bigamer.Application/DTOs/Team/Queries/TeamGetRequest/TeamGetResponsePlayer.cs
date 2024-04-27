namespace Bigamer.Application.DTOs.Team.Queries.TeamGetRequest;

public class TeamGetResponsePlayer
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Nickname { get; set; }
    public string? ImageUrl { get; set; }
}