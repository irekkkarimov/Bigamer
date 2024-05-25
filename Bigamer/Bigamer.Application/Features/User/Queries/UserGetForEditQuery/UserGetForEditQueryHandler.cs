using AutoMapper;
using Bigamer.Application.Common.Exceptions.User;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.User.Queries.UserGetForEditQuery;

public class UserGetForEditQueryHandler : IRequestHandler<UserGetForEditQuery, UserGetAllResponseItem>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserGetForEditQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserGetAllResponseItem> Handle(UserGetForEditQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var userFromDb = await _dbContext.Users
            .Include(i => i.UserInfo)
            .ThenInclude(i => i.Team)
            .FirstOrDefaultAsync(i => i.Id == props.UserId, cancellationToken);

        if (userFromDb is null)
            throw new BadUserException("User not found");

        return _mapper.Map<UserGetAllResponseItem>(userFromDb);
    }
}