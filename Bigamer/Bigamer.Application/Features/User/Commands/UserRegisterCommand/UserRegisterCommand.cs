using Bigamer.Application.DTOs.User.Commands.UserRegister;
using MediatR;

namespace Bigamer.Application.Features.User.Commands.UserRegisterCommand;

public class UserRegisterCommand : IRequest
{
    public UserRegisterCommand(UserRegisterRequest request)
    {
        Props = request;
    }
    
    public UserRegisterRequest Props { get; set; }
}