using Bigamer.Application.DTOs.Team.Commands.TeamAddPlayerRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamAddPlayerCommand;

public class TeamAddPlayerCommand : IRequest
{
    public TeamAddPlayerCommand(TeamAddPlayerRequest request)
    {
        Props = request;
    }

    public TeamAddPlayerRequest Props { get; set; }
}