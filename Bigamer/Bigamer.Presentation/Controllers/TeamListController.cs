using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
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
    public async Task<IActionResult> Index()
    {
        var query = new TeamGetAllQuery();
        var result = await _mediator.Send(query);
        
        return View(result);
    }
}