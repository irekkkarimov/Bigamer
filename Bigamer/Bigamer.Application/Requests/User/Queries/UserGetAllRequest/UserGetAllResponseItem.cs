namespace Bigamer.Application.Requests.User.Queries.UserGetAllRequest;

public class UserGetAllResponseItem
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? TeamName { get; set; }
    public bool IsBanned { get; set; }
}