using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/MatchList")]
public class AdminMatchListController : Controller
{
    private readonly IMediator _mediator;

    public AdminMatchListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET
    public async Task<IActionResult> Index(
        [FromQuery] string? filter, 
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 1)
    {
        var query = new MatchGetAllQuery(new MatchGetAllRequest
        {
            Filter = filter,
            Offset = offset,
            Limit = limit
        });
        var allMatches = await _mediator.Send(query);

        return View(allMatches);
    }
}