using Bigamer.Application.DTOs.Game.Commands.GameAddRequest;
using MediatR;

namespace Bigamer.Application.Features.Game.Commands.GameAddCommand;

public class GameAddCommand : IRequest
{
    public GameAddCommand(GameAddRequest request)
    {
        Props = request;
    }

    public GameAddRequest Props { get; set; }
}