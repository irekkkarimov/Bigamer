using Bigamer.Application.Requests.Subscription.Commands.SubscribeUserRequest;
using MediatR;

namespace Bigamer.Application.Features.Subscription.Commands.SubscribeUserCommand;

public class SubscribeUserCommand : IRequest
{
    public SubscribeUserCommand(SubscribeUserRequest request)
    {
        Props = request;
    }
    
    public SubscribeUserRequest Props { get; set; }
}