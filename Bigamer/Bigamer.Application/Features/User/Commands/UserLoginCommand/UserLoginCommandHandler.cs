using System.Text.RegularExpressions;
using Bigamer.Application.Common.Exceptions.Abstractions;
using Bigamer.Application.Common.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        var user = await _signInManager.UserManager.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Email!.Equals(props.Email), cancellationToken);

        if (user is null)
            throw new BadUserException("User not found");

        if (user.UserInfo.IsBanned)
            throw new BadUserException("You can't login! You're banned!");
        
        var result = await _signInManager.PasswordSignInAsync(user, props.Password, props.RememberMe, false);

        if (!result.Succeeded)
            throw new BadUserException("Wrong password!");
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex MyRegex();
}