using Bigamer.Application.Requests.User.Commands.UserLogin;
using Bigamer.Application.Requests.User.Commands.UserRegister;
using Bigamer.Application.Features.User.Commands.UserLoginCommand;
using Bigamer.Application.Features.User.Commands.UserLogoutCommand;
using Bigamer.Application.Features.User.Commands.UserRegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bigamer.Presentation.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserLoginRequest request)
    {
        var command = new UserLoginCommand(request);
        await _mediator.Send(command);

        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] UserRegisterRequest request)
    {
        var command = new UserRegisterCommand(request);
        await _mediator.Send(command);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        var command = new UserLogoutCommand();
        await _mediator.Send(command);

        return RedirectToAction("Index", "Home");
    }
}