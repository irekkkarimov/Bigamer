using Bigamer.Application.DTOs.Admin;
using Bigamer.Application.Features.Game.Queries.GameGetAllQuery;
using Bigamer.Application.Features.Match.Commands.MatchUpdateTeamStatCommand;
using Bigamer.Application.Features.Team.Commands.TeamAddCommand;
using Bigamer.Application.Features.Team.Commands.TeamDeleteCommand;
using Bigamer.Application.Features.Team.Commands.TeamRemoveLinkCommand;
using Bigamer.Application.Features.Team.Commands.TeamRemovePlayerCommand;
using Bigamer.Application.Features.Team.Commands.TeamUpdateCommand;
using Bigamer.Application.Features.Team.Queries.TeamGetAllQuery;
using Bigamer.Application.Features.Team.Queries.TeamGetForEditQuery;
using Bigamer.Application.Requests.Match.Commands.MatchUpdateTeamStatRequest;
using Bigamer.Application.Requests.Team.Commands.TeamAddRequest;
using Bigamer.Application.Requests.Team.Commands.TeamDeleteRequest;
using Bigamer.Application.Requests.Team.Commands.TeamRemoveLinkRequest;
using Bigamer.Application.Requests.Team.Commands.TeamRemovePlayerRequest;
using Bigamer.Application.Requests.Team.Commands.TeamUpdateRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetForEditRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/Teams")]
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

    [HttpGet]
    [Route("Edit/{teamId:guid}")]
    public async Task<IActionResult> TeamEdit(Guid teamId)
    {
        var query = new TeamGetForEditQuery(new TeamGetForEditRequest
        {
            TeamId = teamId
        });
        var team = await _mediator.Send(query);

        return View(team);
    }

    [HttpGet]
    [Route("Add")]
    public async Task<IActionResult> TeamAdd()
    {
        var query = new GameGetAllQuery();
        var allGames = await _mediator.Send(query);

        var model = new AdminMatchAddModel
        {
            Games = allGames
        };

        return View(model);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> TeamAdd([FromBody] TeamAddRequest request)
    {
        var command = new TeamAddCommand(request);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPatch]
    [Route("Update/{teamId:guid}")]
    public async Task<IActionResult> TeamUpdate([FromRoute] Guid teamId, [FromBody] TeamUpdateRequest request)
    {
        request.TeamId = teamId;
        var command = new TeamUpdateCommand(request);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("DeleteLink/{teamId:guid}")]
    public async Task<IActionResult> DeleteLink([FromRoute] Guid teamId, [FromQuery] string linkType)
    {
        var command = new TeamRemoveLinkCommand(new TeamRemoveLinkRequest
        {
            TeamId = teamId,
            LinkType = linkType
        });
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("RemovePlayer/{teamId:guid}")]
    public async Task<IActionResult> RemovePlayer([FromRoute] Guid teamId, [FromQuery] Guid playerId)
    {
        var command = new TeamRemovePlayerCommand(new TeamRemovePlayerRequest
        {
            TeamId = teamId,
            PlayerId = playerId
        });
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete]
    [Route("Delete/{teamId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid teamId)
    {
        var command = new TeamDeleteCommand(new TeamDeleteRequest
        {
            TeamId = teamId
        });
        await _mediator.Send(command);

        return Ok();
    }

    [HttpPatch]
    [Route("UpdateStat/{matchId:guid}")]
    public async Task<IActionResult> UpdateStat(
        [FromRoute] Guid matchId,
        [FromBody] MatchUpdateTeamStatRequest request)
    {
        request.MatchId = matchId;
        var command = new MatchUpdateTeamStatCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
}