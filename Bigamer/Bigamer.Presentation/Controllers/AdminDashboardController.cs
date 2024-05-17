using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("Admin/Dashboard")]
public class AdminDashboardController : Controller
{
    private readonly IMediator _mediator;

    public AdminDashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        
        return View();
    }
}