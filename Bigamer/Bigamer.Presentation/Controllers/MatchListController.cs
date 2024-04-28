using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class MatchListController : Controller
{
    private readonly IMediator _mediator;

    public MatchListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] string? filter)
    {
        var getAllMatchesQuery = new MatchGetAllQuery(new MatchGetAllRequest
        {
            Filter = filter
        });
        var allMatches = await _mediator.Send(getAllMatchesQuery);
        
        return View(allMatches);
    }
}