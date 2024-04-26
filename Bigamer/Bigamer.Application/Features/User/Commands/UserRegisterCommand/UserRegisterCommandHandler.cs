using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bigamer.Application.Features.User.Commands.UserRegisterCommand;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand>
{
    private readonly UserManager<Domain.Entities.User> _userManager;

    public UserRegisterCommandHandler(UserManager<Domain.Entities.User> userManager)
    {
        _userManager = userManager;
    }

    public Task Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var newUserInfo = new UserInfo
        {
            Id = Guid.NewGuid(),
            TeamId = null,
            Team = null,
            FirstName = props.FirstName,
            LastName = props.LastName,
            NickName = null,
            IsBanned = false,
            ImageUrl = null
        };

        var newUser = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = props.Email
        };

        return Task.FromResult(() => {});
    }
}