using Bigamer.Application.Features.User.Queries.UserGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("Admin/Users")]
public class AdminUserListController : Controller
{
    private readonly IMediator _mediator;

    public AdminUserListController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        var query = new UserGetAllQuery();
        var allUsers = await _mediator.Send(query);
        
        return View(allUsers);
    }
}