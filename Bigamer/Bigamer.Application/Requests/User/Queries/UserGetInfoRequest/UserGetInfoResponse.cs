namespace Bigamer.Application.Requests.User.Queries.UserGetInfoRequest;

public class UserGetInfoResponse
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}