using Bigamer.Application.Requests.Team.Commands;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamAddLinkCommand;

public class TeamAddLinkCommand : IRequest
{
    public TeamAddLinkCommand(TeamAddLinkRequest request)
    {
        Props = request;
    }

    public TeamAddLinkRequest Props { get; set; }
}