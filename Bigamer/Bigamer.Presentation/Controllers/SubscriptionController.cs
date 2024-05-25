using Bigamer.Application.Features.Subscription.Commands.SubscribeUserCommand;
using Bigamer.Application.Features.Subscription.Commands.SubscribeUserConfirmCommand;
using Bigamer.Application.Requests.Subscription.Commands.SubscribeUserConfirmRequest;
using Bigamer.Application.Requests.Subscription.Commands.SubscribeUserRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("[controller]/[action]")]
public class SubscriptionController : Controller
{
    private readonly IMediator _mediator;

    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task SubscribeAsync([FromBody] SubscribeUserRequest request)
    {
        var command = new SubscribeUserCommand(request);
        await _mediator.Send(command);
    }

    [HttpGet]
    public async Task ConfirmAsync([FromQuery] SubscribeUserConfirmRequest request)
    {
        var command = new SubscribeUserConfirmCommand(request);
        await _mediator.Send(command);
    }
}