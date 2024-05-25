using Bigamer.Application.Requests.Match.Commands.MatchAddLinkRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchAddLinkCommand;

public class MatchAddLinkCommand : IRequest
{
    public MatchAddLinkCommand(MatchAddLinkRequest request)
    {
        Props = request;
    }

    public MatchAddLinkRequest Props { get; set; }
}