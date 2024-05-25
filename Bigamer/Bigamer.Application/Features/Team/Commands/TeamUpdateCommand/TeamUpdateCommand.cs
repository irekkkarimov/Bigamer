using Bigamer.Application.Requests.Team.Commands.TeamUpdateRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamUpdateCommand;

public class TeamUpdateCommand : IRequest
{
    public TeamUpdateCommand(TeamUpdateRequest request)
    {
        Props = request;
    }
    
    public TeamUpdateRequest Props { get; set; }
}