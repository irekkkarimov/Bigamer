using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

public class GameListController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}