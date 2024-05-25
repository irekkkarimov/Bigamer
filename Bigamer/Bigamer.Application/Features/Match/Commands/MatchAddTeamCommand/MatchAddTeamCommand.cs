using Bigamer.Application.Requests.Match.Commands.MatchAddTeamRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchAddTeamCommand;

public class MatchAddTeamCommand : IRequest
{
    public MatchAddTeamCommand(MatchAddTeamRequest props)
    {
        Props = props;
    }

    public MatchAddTeamRequest Props { get; set; }
}