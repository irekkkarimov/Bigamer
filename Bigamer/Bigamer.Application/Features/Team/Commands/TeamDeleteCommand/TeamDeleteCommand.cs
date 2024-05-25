using Bigamer.Application.Requests.Team.Commands.TeamDeleteRequest;
using MediatR;

namespace Bigamer.Application.Features.Team.Commands.TeamDeleteCommand;

public class TeamDeleteCommand : IRequest
{
    public TeamDeleteCommand(TeamDeleteRequest props)
    {
        Props = props;
    }

    public TeamDeleteRequest Props { get; set; }
}