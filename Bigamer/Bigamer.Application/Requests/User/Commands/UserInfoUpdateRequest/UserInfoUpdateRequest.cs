namespace Bigamer.Application.Requests.User.Commands.UserInfoUpdateRequest;

public class UserInfoUpdateRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}