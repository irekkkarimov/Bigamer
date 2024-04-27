using Bigamer.Application.Common.Exceptions.Game;
using Bigamer.Application.Interfaces;
using MediatR;

namespace Bigamer.Application.Features.Game.Commands.GameAddCommand;

public class GameAddCommandHandler : IRequestHandler<GameAddCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public GameAddCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(GameAddCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (string.IsNullOrWhiteSpace(props.Name))
            throw new GameValidationException("Wrong game name");

        var newGame = new Domain.Entities.Game
        {
            Id = Guid.NewGuid(),
            Name = props.Name
        };

        await _dbContext.Games.AddAsync(newGame, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}