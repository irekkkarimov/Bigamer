using Bigamer.Application.Common.Exceptions.Team;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Team.Commands.TeamDeleteCommand;

public class TeamDeleteCommandHandler : IRequestHandler<TeamDeleteCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public TeamDeleteCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(TeamDeleteCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var teamFromDb = await _dbContext.Teams
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);

        if (teamFromDb is null)
            throw new TeamBadRequest("Team not found");

        _dbContext.Teams.Remove(teamFromDb);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}