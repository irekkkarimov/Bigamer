using System.Diagnostics;
using Bigamer.Application.DTOs.Home.GetInfo;
using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;
using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery.MatchGetRandomActiveQuery;
using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Microsoft.AspNetCore.Mvc;
using Bigamer.Presentation.Models;
using MediatR;

namespace Bigamer.Presentation.Controllers;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var getRandomActiveMatchQuery = new MatchGetRandomActiveQuery();
        var randomActiveMatch = await _mediator.Send(getRandomActiveMatchQuery);
        
        var getAllTeamsQuery = new TeamGetAllQuery();
        var getAllTeamsResponse = await _mediator.Send(getAllTeamsQuery);

        var getAllMatchesQuery = new MatchGetAllQuery(new MatchGetAllRequest());
        var getAllMatchesResponse = await _mediator.Send(getAllMatchesQuery);
        
        var getHomePageInfoDto = new GetHomePageInfoDto
        {
            TodayMatch = randomActiveMatch,
            TeamsInfo = getAllTeamsResponse,
            MatchesInfo = getAllMatchesResponse
        };

        return View(getHomePageInfoDto);
    }
}