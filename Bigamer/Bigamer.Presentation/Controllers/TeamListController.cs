using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class TeamListController : Controller
{
    private readonly IMediator _mediator;

    public TeamListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int offset = 0, [FromQuery] int limit = 6)
    {
        var query = new TeamGetAllQuery(new TeamGetAllRequest
        {
            Offset = offset,
            Limit = limit
        });
        var result = await _mediator.Send(query);
        
        return View(result);
    }
}