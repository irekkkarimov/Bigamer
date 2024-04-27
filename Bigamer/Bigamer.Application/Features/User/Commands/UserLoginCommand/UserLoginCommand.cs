using Bigamer.Application.DTOs.User.Commands.UserLogin;
using Bigamer.Application.DTOs.User.Commands.UserRegister;
using MediatR;

namespace Bigamer.Application.Features.User.Commands.UserLoginCommand;

public class UserLoginCommand : IRequest
{
    public UserLoginCommand(UserLoginRequest request)
    {
        Props = request;
    }

    public UserLoginRequest Props { get; set; }
}