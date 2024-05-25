using Bigamer.Application.Features.User.Commands.UserEditCommand;
using Bigamer.Application.Features.User.Queries.UserGetAllQuery;
using Bigamer.Application.Features.User.Queries.UserGetForEditQuery;
using Bigamer.Application.Features.User.Queries.UserGetInfoQuery;
using Bigamer.Application.Requests.User.Commands.UserEditRequest;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using Bigamer.Application.Requests.User.Queries.UserGetForEditRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/Users")]
public class AdminUserListController : Controller
{
    private readonly IMediator _mediator;

    public AdminUserListController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index([FromQuery] int offset = 0, [FromQuery] int limit = 5)
    {
        var query = new UserGetAllQuery(new UserGetAllRequest
        {
            Offset = offset,
            Limit = limit
        });
        var allUsers = await _mediator.Send(query);
        
        return View(allUsers);
    }

    [HttpGet]
    [Route("Edit/{userId:guid}")]
    public async Task<IActionResult> UserEdit(Guid userId)
    {
        var query = new UserGetForEditQuery(new UserGetForEditRequest
        {
            UserId = userId
        });
        var user = await _mediator.Send(query);
        
        return View(user);
    }

    [HttpPatch]
    [Route("Update/{userId:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UserEditRequest request)
    {
        request.UserId = userId;
        var command = new UserEditCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
}