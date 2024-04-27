using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bigamer.Application.Features.User.Commands.UserLogoutCommand;

public class UserLogoutCommandHandler : IRequestHandler<UserLogoutCommand>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public UserLogoutCommandHandler(SignInManager<Domain.Entities.User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}