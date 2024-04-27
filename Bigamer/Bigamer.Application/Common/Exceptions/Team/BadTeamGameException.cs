using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Team;

public class BadTeamGameException : BadRequestException
{
    public BadTeamGameException(string message) : base(message)
    {
    }
}