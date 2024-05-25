namespace Bigamer.Application.Requests.User.Commands.UserEditRequest;

public class UserEditRequest
{
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? Image { get; set; }
    public bool? IsBanned { get; set; } = null;
}