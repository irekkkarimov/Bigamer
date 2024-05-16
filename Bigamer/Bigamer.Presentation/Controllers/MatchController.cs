using Bigamer.Application.Features.Match.Commands.MatchAddCommand;
using Bigamer.Application.Requests.Match.Commands.MatchAddRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("api/[controller]/[action]")]
public class MatchController : ControllerBase
{
    private readonly IMediator _mediator;

    public MatchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MatchAddRequest request)
    {
        Console.WriteLine(request is null);
        var command = new MatchAddCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
}