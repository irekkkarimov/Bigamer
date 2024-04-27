using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Team;

public class TeamBadRequest : BadRequestException
{
    public TeamBadRequest(string message) : base(message)
    {
    }
}