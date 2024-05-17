using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

public class AdminMatchListController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}