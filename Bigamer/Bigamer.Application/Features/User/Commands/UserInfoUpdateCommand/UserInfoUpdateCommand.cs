using Bigamer.Application.Requests.User.Commands.UserInfoUpdateRequest;
using MediatR;

namespace Bigamer.Application.Features.User.Commands.UserInfoUpdateCommand;

public class UserInfoUpdateCommand : IRequest
{
    public UserInfoUpdateCommand(UserInfoUpdateRequest props)
    {
        Props = props;
    }

    public UserInfoUpdateRequest Props { get; set; }
}