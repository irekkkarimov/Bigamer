using Bigamer.Application.Requests.Match.Commands.MatchDeleteRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchDeleteCommand;

public class MatchDeleteCommand : IRequest
{
    public MatchDeleteCommand(MatchDeleteRequest props)
    {
        Props = props;
    }

    public MatchDeleteRequest Props { get; set; }
}