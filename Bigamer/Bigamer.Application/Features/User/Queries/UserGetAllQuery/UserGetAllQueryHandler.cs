using AutoMapper;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Queries.UserGetAllQuery;

public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, UserGetAllResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public UserGetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper,
        UserManager<Domain.Entities.User> userManager)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserGetAllResponse> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
    {
        var allUsersFromDb = await _dbContext.Users
            .Include(i => i.UserInfo)
            .ThenInclude(i => i.Team)
            .ToListAsync(cancellationToken);

        var usersWithRoles = new List<(Domain.Entities.User, string)>();

        foreach (var user in allUsersFromDb)
        {
            var rolePriority = new Dictionary<string, int>()
            {
                { "user", 0 },
                { "player", 1 },
                { "admin", 2 }
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = "user";
            if (userRoles.Count == 1)
                userRole = userRoles.First();
            else
                foreach (var role in userRoles)
                    if (rolePriority[role] > rolePriority[userRole])
                        userRole = role;

            usersWithRoles.Add((user, userRole));
        }

        var mappedUsers = usersWithRoles
            .Select(i =>
            {
                var mappedUser = _mapper.Map<UserGetAllResponseItem>(i.Item1);
                mappedUser.Role = i.Item2;
                return mappedUser;
            })
            .ToList();

        return new UserGetAllResponse
        {
            Users = mappedUsers
        };
    }
}