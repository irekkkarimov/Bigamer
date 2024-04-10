using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

public class TeamController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}