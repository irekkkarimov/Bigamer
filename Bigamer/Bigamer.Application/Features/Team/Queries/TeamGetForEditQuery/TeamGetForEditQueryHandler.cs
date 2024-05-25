using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.DTOs.Links;
using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Team.Queries.TeamGetForEditRequest;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Queries.TeamGetForEditQuery;

public class TeamGetForEditQueryHandler : IRequestHandler<TeamGetForEditQuery, TeamGetForEditResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamGetForEditQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TeamGetForEditResponse> Handle(TeamGetForEditQuery request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .Include(i => i.Game)
            .Include(i => i.TeamLinks)
            .Include(i => i.UserInfos)
            .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamNotFoundException("Team not found");

        var result = new TeamGetForEditResponse
        {
            TeamId = teamFromDb.Id,
            TeamName = teamFromDb.Name,
            GameName = teamFromDb.Game.Name,
            ImageUrl = teamFromDb.TeamInfo.ImageUrl ?? "",
            Links = teamFromDb.TeamLinks
                .Select(i => new GetLink
                {
                    ServiceName = i.ServiceName,
                    Link = i.Link
                })
                .ToList(),
            Players = teamFromDb.UserInfos
                .Select(i => new UserGetAllResponseItem
                {
                    UserId = i.UserId,
                    Email = i.User.Email!,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Nickname = i.User.UserName,
                    Role = "player",
                    TeamName = teamFromDb.Name,
                    IsBanned = i.IsBanned
                })
                .ToList()
        };

        return result;
    }
}