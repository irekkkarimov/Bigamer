using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

public class TeamListController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}