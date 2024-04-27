namespace Bigamer.Application.DTOs.User.Commands.UserLogin;

public class UserLoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}