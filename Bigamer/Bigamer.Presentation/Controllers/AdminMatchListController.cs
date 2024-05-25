using Bigamer.Application.DTOs.Admin;
using Bigamer.Application.Features.Game.Queries.GameGetAllQuery;
using Bigamer.Application.Features.Match.Commands.MatchAddCommand;
using Bigamer.Application.Features.Match.Commands.MatchAddLinkCommand;
using Bigamer.Application.Features.Match.Commands.MatchAddTeamCommand;
using Bigamer.Application.Features.Match.Commands.MatchDeleteCommand;
using Bigamer.Application.Features.Match.Commands.MatchRemoveLinkCommand;
using Bigamer.Application.Features.Match.Commands.MatchRemoveTeamCommand;
using Bigamer.Application.Features.Match.Commands.MatchUpdateCommand;
using Bigamer.Application.Features.Match.Queries.MatchGetAllQuery;
using Bigamer.Application.Features.Match.Queries.MatchGetForEditQuery;
using Bigamer.Application.Features.Team.Queries.MatchGetForStatEditQuery;
using Bigamer.Application.Requests.Match.Commands.MatchAddLinkRequest;
using Bigamer.Application.Requests.Match.Commands.MatchAddTeamRequest;
using Bigamer.Application.Requests.Match.Commands.MatchDeleteRequest;
using Bigamer.Application.Requests.Match.Commands.MatchRemoveLinkRequest;
using Bigamer.Application.Requests.Match.Commands.MatchRemoveTeamRequest;
using Bigamer.Application.Requests.Match.Commands.MatchUpdateRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetForEditRequest;
using Bigamer.Application.Requests.Team.Queries.MatchGetForStatEditRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/Matches")]
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
        [FromQuery] int limit = 5)
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

    [HttpGet]
    [Route("Add")]
    public async Task<IActionResult> MatchAdd()
    {
        var getGamesQuery = new GameGetAllQuery();
        var games = await _mediator.Send(getGamesQuery);

        var model = new AdminMatchAddModel
        {
            Games = games
        };

        return View(model);
    }

    [HttpGet]
    [Route("Edit/{matchId:guid}")]
    public async Task<IActionResult> MatchEdit([FromRoute] Guid matchId)
    {
        var query = new MatchGetForEditQuery(new MatchGetForEditRequest
        {
            MatchId = matchId
        });
        var match = await _mediator.Send(query);
        Console.WriteLine(match.StartDate);
        return View(match);
    }

    [HttpGet]
    [Route("EditStat/{matchId:guid}")]
    public async Task<IActionResult> MatchTeamEdit(
        [FromRoute] Guid matchId,
        [FromQuery] Guid teamId)
    {
        var query = new MatchGetForStatEditQuery(new MatchGetForStatEditRequest
        {
            MatchId = matchId,
            TeamId = teamId
        });
        var teamStats = await _mediator.Send(query);

        return View(teamStats);
    }

    [HttpPatch]
    [Route("Update/{matchId:guid}")]
    public async Task<IActionResult> MatchUpdate(
        [FromRoute] Guid matchId,
        [FromBody] MatchUpdateRequest request)
    {
        request.MatchId = matchId;
        var command = new MatchUpdateCommand(request);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("DeleteLink/{matchId:guid}")]
    public async Task<IActionResult> DeleteLink(
        [FromRoute] Guid matchId, 
        [FromQuery] string linkType)
    {
        var command = new MatchRemoveLinkCommand(new MatchRemoveLinkRequest
        {
            MatchId = matchId,
            ServiceName = linkType
        });
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("RemoveTeam/{matchId:guid}")]
    public async Task<IActionResult> RemoveTeam(
        [FromRoute] Guid matchId,
        [FromQuery] Guid teamId)
    {
        var command = new MatchRemoveTeamCommand(new MatchRemoveTeamRequest
        {
            MatchId = matchId,
            TeamId = teamId
        });
        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpPost]
    [Route("AddLink")]
    public async Task<IActionResult> AddLink([FromBody] MatchAddLinkRequest request)
    {
        var command = new MatchAddLinkCommand(request);
        await _mediator.Send(command);
        
        return Ok();
    }
    
    [HttpPost]
    [Route("AddTeam")]
    public async Task<IActionResult> AddTeam([FromBody] MatchAddTeamRequest request)
    {
        var command = new MatchAddTeamCommand(request);
        await _mediator.Send(command);
        
        return Ok();
    }

    [HttpDelete]
    [Route("Delete/{matchId:guid}")]
    public async Task<IActionResult> DeleteMatch([FromRoute] Guid matchId)
    {
        var command = new MatchDeleteCommand(new MatchDeleteRequest
        {
            MatchId = matchId
        });
        await _mediator.Send(command);

        return Ok();
    }
}