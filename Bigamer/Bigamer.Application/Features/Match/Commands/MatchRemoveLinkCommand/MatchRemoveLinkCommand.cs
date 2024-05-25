using Bigamer.Application.Requests.Match.Commands.MatchRemoveLinkRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchRemoveLinkCommand;

public class MatchRemoveLinkCommand : IRequest
{
    public MatchRemoveLinkCommand(MatchRemoveLinkRequest props)
    {
        Props = props;
    }

    public MatchRemoveLinkRequest Props { get; set; }
}