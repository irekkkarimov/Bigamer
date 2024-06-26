﻿using System.Diagnostics;
using Bigamer.Application.DTOs.Home.GetInfo;
using Bigamer.Application.Features.Match.Queries.MatchGetAllForHomeQuery;
using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;
using Bigamer.Application.Features.Match.Queries.MatchGetRandomActiveQuery;
using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
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
        
        var getAllTeamsQuery = new TeamGetAllQuery(new TeamGetAllRequest
        {
            Offset = 0,
            Limit = 15
        });
        var getAllTeamsResponse = await _mediator.Send(getAllTeamsQuery);

        var getAllMatchesQuery = new MatchGetAllForHomeQuery();
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