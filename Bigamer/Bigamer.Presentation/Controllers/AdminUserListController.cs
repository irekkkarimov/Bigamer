using Bigamer.Application.Features.User.Queries.UserGetAllQuery;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
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
}