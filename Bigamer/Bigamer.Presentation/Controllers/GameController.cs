using Bigamer.Application.DTOs.Game.Commands.GameAddRequest;
using Bigamer.Application.Features.Game.Commands.GameAddCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] GameAddRequest request)
    {
        var command = new GameAddCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
}