using Bigamer.Application.Features.User.Queries.UserGetInfoQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("Admin/Profile")]
public class AdminProfileController : Controller
{
    private readonly IMediator _mediator;

    public AdminProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var query = new UserGetInfoQuery();
        var userInfo = await _mediator.Send(query);
        
        return View(userInfo);
    }
}