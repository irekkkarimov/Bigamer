using Bigamer.Application.Requests.Subscription.Commands.SubscribeUserConfirmRequest;
using MediatR;

namespace Bigamer.Application.Features.Subscription.Commands.SubscribeUserConfirmCommand;

public class SubscribeUserConfirmCommand : IRequest
{
    public SubscribeUserConfirmCommand(SubscribeUserConfirmRequest request)
    {
        Props = request;
    }

    public SubscribeUserConfirmRequest Props { get; set; }
}