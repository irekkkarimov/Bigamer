using Bigamer.Application.Requests.Match.Commands.MatchAddRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchAddCommand;

public class MatchAddCommand : IRequest
{
    public MatchAddCommand(MatchAddRequest request)
    {
        Props = request;
    }

    public MatchAddRequest Props { get; set; }
}