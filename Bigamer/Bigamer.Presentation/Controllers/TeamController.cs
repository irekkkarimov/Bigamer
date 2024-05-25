using Bigamer.Application.Requests.Team.Commands;
using Bigamer.Application.Requests.Team.Commands.TeamAddPlayerRequest;
using Bigamer.Application.Requests.Team.Commands.TeamAddRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetRequest;
using Bigamer.Application.Features.Team.Commands.TeamAddCommand;
using Bigamer.Application.Features.Team.Commands.TeamAddLinkCommand;
using Bigamer.Application.Features.Team.Commands.TeamAddPlayerCommand;
using Bigamer.Application.Features.Team.Queries.TeamGetQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

// [Authorize]
public class TeamController : Controller
{
    private readonly IMediator _mediator;

    public TeamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("[controller]/[action]/{teamId:guid}")]
    public async Task<IActionResult> Index(Guid teamId)
    {
        var query = new TeamGetQuery(new TeamGetRequest
        {
            TeamId = teamId
        });
        var response = await _mediator.Send(query);
        
        return View(response);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [Route("api/[controller]/[action]")]
    public async Task<IActionResult> AddAsync([FromBody] TeamAddRequest request)
    {
        var command = new TeamAddCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    [Route("api/[controller]/[action]")]
    public async Task<IActionResult> AddPlayerAsync([FromBody] TeamAddPlayerRequest request)
    {
        var command = new TeamAddPlayerCommand(request);
        await _mediator.Send(command);
 
        return Ok();
    }
    
    // [Authorize(Roles = "admin")]
    [HttpPost]
    [Route("api/[controller]/[action]")]
    public async Task<IActionResult> AddLinkAsync([FromBody] TeamAddLinkRequest request)
    {
        var command = new TeamAddLinkCommand(request);
        await _mediator.Send(command);
 
        return Ok();
    }
}