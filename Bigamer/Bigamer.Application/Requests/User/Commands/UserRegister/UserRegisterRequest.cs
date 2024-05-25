namespace Bigamer.Application.Requests.User.Commands.UserRegister;

public class UserRegisterRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public bool IsPlayer { get; set; }
}