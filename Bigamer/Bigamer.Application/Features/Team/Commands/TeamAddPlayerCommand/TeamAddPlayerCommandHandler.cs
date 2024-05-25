using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Common.Exceptions.User;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamAddPlayerCommand;

public class TeamAddPlayerCommandHandler : IRequestHandler<TeamAddPlayerCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public TeamAddPlayerCommandHandler(IApplicationDbContext dbContext, UserManager<Domain.Entities.User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task Handle(TeamAddPlayerCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (string.IsNullOrWhiteSpace(props.PlayerEmail))
            throw new TeamBadRequest("Wrong email format!");
        
        var teamFromDb = await _dbContext.Teams
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamBadRequest("Team not found");

        var playerFromDb = await _dbContext.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Email == props.PlayerEmail, cancellationToken);

        if (playerFromDb is null)
            throw new BadUserException("Player not found");

        var playerRoles = await _userManager.GetRolesAsync(playerFromDb);

        if (!playerRoles.Select(i => i.ToLower()).Contains("player"))
            throw new TeamBadRequest("User is not a Player");
        
        if (playerFromDb.UserInfo.TeamId is not null)
        {
            if (playerFromDb.UserInfo.TeamId.Value == teamFromDb.Id)
                throw new TeamBadRequest("User is already in this team");
            
            throw new TeamBadRequest("User is already in some team");
        }

        playerFromDb.UserInfo.Team = teamFromDb;
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}