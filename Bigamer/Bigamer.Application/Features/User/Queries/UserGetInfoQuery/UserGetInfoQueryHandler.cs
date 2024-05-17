using System.Security.Claims;
using Bigamer.Application.Requests.User.Queries.UserGetInfoRequest;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Queries.UserGetInfoQuery;

public class UserGetInfoQueryHandler : IRequestHandler<UserGetInfoQuery, UserGetInfoResponse>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public UserGetInfoQueryHandler(SignInManager<Domain.Entities.User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<UserGetInfoResponse> Handle(UserGetInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var userFromDb = await _signInManager.UserManager.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (userFromDb is null)
            throw new ArgumentException("Current User not found");

        return new UserGetInfoResponse
        {
            FirstName = userFromDb.UserInfo.FirstName,
            LastName = userFromDb.UserInfo.LastName,
            UserName = userFromDb.UserName ?? "null",
            Email = userFromDb.Email ?? "null"
        };
    }
    
}