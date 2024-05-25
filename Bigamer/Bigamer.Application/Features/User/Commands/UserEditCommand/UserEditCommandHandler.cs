using System.Data.Common;
using System.Security.Claims;
using Bigamer.Application.Common.Exceptions.User;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Commands.UserEditCommand;

public class UserEditCommandHandler : IRequestHandler<UserEditCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public UserEditCommandHandler(IApplicationDbContext dbContext, SignInManager<Domain.Entities.User> signInManager)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
    }

    public async Task Handle(UserEditCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var userFromDb = await _dbContext.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Id == props.UserId, cancellationToken);

        if (userFromDb is null)
            throw new BadUserException("User not found");
        
        if (!string.IsNullOrWhiteSpace(props.FirstName))
            userFromDb.UserInfo.FirstName = props.FirstName;

        if (!string.IsNullOrWhiteSpace(props.LastName))
            userFromDb.UserInfo.LastName = props.LastName;
        
        if (!string.IsNullOrWhiteSpace(props.Nickname))
            userFromDb.UserInfo.NickName = props.Nickname;
        
        if (!string.IsNullOrWhiteSpace(props.Image))
            userFromDb.UserInfo.ImageUrl = props.Image;

        if (props.IsBanned is not null)
        {
            var currentUserIdClaim = _signInManager.Context.User.Claims
                .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

            if (currentUserIdClaim is null)
                throw new BadUserException("Current User Id Claim not found");

            if (userFromDb.Id == new Guid(currentUserIdClaim.Value))
                throw new BadUserException("You can't ban yourself!");
            
            userFromDb.UserInfo.IsBanned = props.IsBanned.Value;
        }

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}