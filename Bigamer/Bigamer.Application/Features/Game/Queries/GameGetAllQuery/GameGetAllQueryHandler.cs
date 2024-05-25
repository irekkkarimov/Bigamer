using Bigamer.Application.Interfaces;
using Bigamer.Application.Requests.Game.Queries.GameGetAllRequest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Game.Queries.GameGetAllQuery;

public class GameGetAllQueryHandler: IRequestHandler<GameGetAllQuery, GameGetAllResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GameGetAllQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GameGetAllResponse> Handle(GameGetAllQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.Games
            .Select(i => new GameGetAllResponseItem
            {
                GameId = i.Id,
                GameName = i.Name
            })
            .ToListAsync(cancellationToken);

        return new GameGetAllResponse
        {
            Games = games
        };
    }
}