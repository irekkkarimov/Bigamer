using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Team;

public class TeamNotFoundException : NotFoundException
{
    public TeamNotFoundException(string message) : base(message)
    {
    }
}