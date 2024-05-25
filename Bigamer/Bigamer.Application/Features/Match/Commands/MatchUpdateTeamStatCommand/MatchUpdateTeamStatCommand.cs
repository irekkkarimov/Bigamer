using Bigamer.Application.Requests.Match.Commands.MatchUpdateTeamStatRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchUpdateTeamStatCommand;

public class MatchUpdateTeamStatCommand : IRequest
{
    public MatchUpdateTeamStatCommand(MatchUpdateTeamStatRequest props)
    {
        Props = props;
    }

    public MatchUpdateTeamStatRequest Props { get; set; }
}