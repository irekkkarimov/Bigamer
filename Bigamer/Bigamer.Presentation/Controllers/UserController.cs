using Bigamer.Application.Features.User.Commands.UserInfoUpdateCommand;
using Bigamer.Application.Requests.User.Commands.UserInfoUpdateRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UserInfoUpdateRequest request)
    {
        var query = new UserInfoUpdateCommand(request);
        await _mediator.Send(query);

        return Ok();
    }
}