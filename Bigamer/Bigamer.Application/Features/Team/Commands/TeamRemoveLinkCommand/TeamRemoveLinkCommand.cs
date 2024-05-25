using Bigamer.Application.Requests.Team.Commands.TeamRemoveLinkRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamRemoveLinkCommand;

public class TeamRemoveLinkCommand : IRequest
{
    public TeamRemoveLinkCommand(TeamRemoveLinkRequest request)
    {
        Props = request;
    }
    
    public TeamRemoveLinkRequest Props { get; set; }
}