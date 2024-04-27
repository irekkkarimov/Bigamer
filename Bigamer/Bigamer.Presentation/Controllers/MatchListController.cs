using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class MatchListController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}