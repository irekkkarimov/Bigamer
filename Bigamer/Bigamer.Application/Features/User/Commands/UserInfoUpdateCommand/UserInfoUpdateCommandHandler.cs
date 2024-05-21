using System.Security.Claims;
using System.Text.RegularExpressions;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Commands.UserInfoUpdateCommand;

public partial class UserInfoUpdateCommandHandler : IRequestHandler<UserInfoUpdateCommand>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IApplicationDbContext _dbContext;
    private readonly Regex _emailRegex = MyRegex();

    public UserInfoUpdateCommandHandler(SignInManager<Domain.Entities.User> signInManager, IApplicationDbContext dbContext)
    {
        _signInManager = signInManager;
        _dbContext = dbContext;
    }

    public async Task Handle(UserInfoUpdateCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id claim not found");

        var currentUser = await _dbContext.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (currentUser is null)
            throw new ArgumentException("Current User not found");

        if (!string.IsNullOrWhiteSpace(props.FirstName))
            currentUser.UserInfo.FirstName = props.FirstName;

        if (!string.IsNullOrWhiteSpace(props.LastName))
            currentUser.UserInfo.LastName = props.LastName;

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
        
        // if (!string.IsNullOrWhiteSpace(props.Email) && _emailRegex.IsMatch(props.Email))
        //     if (await _signInManager.UserManager.Users
        //             .AnyAsync(i => i.Email!.ToLower().Equals(props.Email.ToLower()), cancellationToken))
        //     {
        //         var token = await _signInManager.UserManager
        //             .GenerateChangeEmailTokenAsync(currentUser, props.Email);
        //
        //         await _signInManager.UserManager.ChangeEmailAsync(currentUser, props.Email, token);
        //     }

        if (!string.IsNullOrWhiteSpace(props.OldPassword) && !string.IsNullOrWhiteSpace(props.NewPassword))
        {
            var result = await _signInManager.UserManager
                .ChangePasswordAsync(currentUser, props.OldPassword, props.NewPassword);

            if (!result.Succeeded)
                throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
        }
    }

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex MyRegex();
}