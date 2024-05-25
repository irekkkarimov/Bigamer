using System.Text.Json;
using System.Text.RegularExpressions;
using Bigamer.Application.Common.Exceptions.User;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Commands.UserRegisterCommand;

public partial class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand>
{
    private readonly Regex _emailRegex = MyRegex();
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IApplicationDbContext _dbContext;

    public UserRegisterCommandHandler(UserManager<Domain.Entities.User> userManager, IApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public async Task Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
       
        if (!_emailRegex.IsMatch(props.Email))
            throw new UserValidationException("Wrong email format");

        var tryFindUser = await _userManager.FindByEmailAsync(props.Email);

        if (tryFindUser is not null)
            throw new BadUserException("User with this email exists");
        
        if (string.IsNullOrWhiteSpace(props.Nickname))
            throw new UserValidationException("Wrong nickname");
        
        if (await _dbContext.UserInfos
                .AnyAsync(i => i.NickName == props.Nickname, cancellationToken: cancellationToken))
            throw new BadUserException("User with this nickname exists");
        
        if (string.IsNullOrWhiteSpace(props.FirstName))
            throw new UserValidationException("Wrong first name");

        if (string.IsNullOrWhiteSpace(props.LastName))
            throw new UserValidationException("Wrong last name");

        if (string.IsNullOrWhiteSpace(props.Password) || string.IsNullOrWhiteSpace(props.ConfirmPassword))
            throw new UserValidationException("Password is empty");

        if (!props.Password.Equals(props.ConfirmPassword))
            throw new UserValidationException("Passwords don't match");
        
        var newUser = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = props.Email
        };

        newUser.UserName = newUser.Id.ToString();

        var result = await _userManager.CreateAsync(newUser, props.Password);

        if (!result.Succeeded)
            throw new BadUserException(result.Errors.FirstOrDefault()?.Description 
                                              ?? "Registration was not successful");

        if (props.IsPlayer)
            await _userManager.AddToRoleAsync(newUser, "player");
        else
            await _userManager.AddToRoleAsync(newUser, "user");
        
        var newUserInfo = new UserInfo
        {
            User = newUser,
            TeamId = null,
            Team = null,
            FirstName = props.FirstName,
            LastName = props.LastName,
            NickName = props.Nickname,
            IsBanned = false,
            ImageUrl = null
        };

        _dbContext.UserInfos.Add(newUserInfo);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex MyRegex();
}