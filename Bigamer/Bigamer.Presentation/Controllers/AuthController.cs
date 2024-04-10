using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
}