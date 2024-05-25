using Bigamer.Application.DTOs.Admin;
using Bigamer.Application.Features.Admin.Queries.GetDashboardStatisticsQuery;
using Bigamer.Application.Features.Match.Queries.MatchGetRecentQuery;
using Bigamer.Application.Requests.Match.Queries.MatchGetRecentRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/Dashboard")]
public class AdminDashboardController : Controller
{
    private readonly IMediator _mediator;

    public AdminDashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var getStatisticsQuery = new GetDashboardStatisticsQuery();
        var getStatisticsResponse = await _mediator.Send(getStatisticsQuery);

        var getRecentMatchesQuery = new MatchGetRecentQuery(new MatchGetRecentRequest
        {
            MatchCount = 7
        });
        var getRecentMatchesResponse = await _mediator.Send(getRecentMatchesQuery);
        

        var newDashboardModel = new AdminDashboardModel
        {
            Statistics = getStatisticsResponse,
            RecentMatches = getRecentMatchesResponse
        };

        return View(newDashboardModel);
    }
}