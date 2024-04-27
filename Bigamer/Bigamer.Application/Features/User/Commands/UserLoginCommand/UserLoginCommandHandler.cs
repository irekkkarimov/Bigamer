using System.Text.RegularExpressions;
using Bigamer.Application.Common.Exceptions.Abstractions;
using Bigamer.Application.Common.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bigamer.Application.Features.User.Commands.UserLoginCommand;

public partial class UserLoginCommandHandler : IRequestHandler<UserLoginCommand>
{
    private readonly Regex _emailRegex = MyRegex();
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public UserLoginCommandHandler(SignInManager<Domain.Entities.User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (props.Email is null || !_emailRegex.IsMatch(props.Email))
            throw new UserValidationException("Wrong email format");

        var user = await _signInManager.UserManager.FindByEmailAsync(props.Email);

        if (user is null)
            throw new BadRequestException("User not found");

        await _signInManager.SignInAsync(user, props.RememberMe);
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex MyRegex();
}