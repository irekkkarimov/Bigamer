using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Match;

public class MatchNotFoundException : NotFoundException
{
    public MatchNotFoundException(string message) : base(message)
    {
    }
}