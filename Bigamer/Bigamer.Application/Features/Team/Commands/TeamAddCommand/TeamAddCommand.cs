using Bigamer.Application.DTOs.Team.Commands.TeamAddRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamAddCommand;

public class TeamAddCommand : IRequest
{
    public TeamAddCommand(TeamAddRequest request)
    {
        Props = request;
    }

    public TeamAddRequest Props { get; set; }
}