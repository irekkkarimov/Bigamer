using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Match;

public class MatchBadRequestException : BadRequestException
{
    public MatchBadRequestException(string message) : base(message)
    {
    }
}