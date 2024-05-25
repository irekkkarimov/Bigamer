using Bigamer.Application.Requests.User.Commands.UserEditRequest;
using MediatR;

namespace Bigamer.Application.Features.User.Commands.UserEditCommand;

public class UserEditCommand : IRequest
{
    public UserEditCommand(UserEditRequest request)
    {
        Props = request;
    }
    
    public UserEditRequest Props { get; set; }
}