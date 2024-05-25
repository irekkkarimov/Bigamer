namespace Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

public class UserGetAllResponseItem
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Nickname { get; set; }
    public string Role { get; set; } = null!;
    public string? TeamName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsBanned { get; set; }
}