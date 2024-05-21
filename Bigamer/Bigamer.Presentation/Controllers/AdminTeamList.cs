using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/TeamList")]
public class AdminTeamList : Controller
{
    private readonly IMediator _mediator;

    public AdminTeamList(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index([FromQuery] int offset = 0, [FromQuery] int limit = 2)
    {
        var query = new TeamGetAllQuery(new TeamGetAllRequest
        {
            Offset = offset,
            Limit = limit
        });
        var allTeams = await _mediator.Send(query);
        
        return View(allTeams);
    }
}