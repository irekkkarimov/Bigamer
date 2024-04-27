using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bigamer.Presentation.Models;

namespace Bigamer.Presentation.Controllers;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}