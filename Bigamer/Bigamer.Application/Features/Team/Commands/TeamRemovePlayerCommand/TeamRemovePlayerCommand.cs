using Bigamer.Application.Requests.Team.Commands.TeamRemovePlayerRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamRemovePlayerCommand;

public class TeamRemovePlayerCommand : IRequest
{
    public TeamRemovePlayerCommand(TeamRemovePlayerRequest request)
    {
        Props = request;
    }
    
    public TeamRemovePlayerRequest Props { get; set; }
}