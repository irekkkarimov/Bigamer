using Bigamer.Application.Requests.Match.Commands.MatchUpdateRequest;
using MediatR;

namespace Bigamer.Application.Features.Match.Commands.MatchUpdateCommand;

public class MatchUpdateCommand : IRequest
{
    public MatchUpdateCommand(MatchUpdateRequest request)
    {
        Props = request;
    }
    
    public MatchUpdateRequest Props { get; set; }
}