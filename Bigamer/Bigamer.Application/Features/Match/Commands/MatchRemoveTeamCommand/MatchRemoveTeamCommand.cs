using Bigamer.Application.Requests.Match.Commands.MatchRemoveTeamRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchRemoveTeamCommand;

public class MatchRemoveTeamCommand : IRequest
{
    public MatchRemoveTeamCommand(MatchRemoveTeamRequest props)
    {
        Props = props;
    }

    public MatchRemoveTeamRequest Props { get; set; }
}