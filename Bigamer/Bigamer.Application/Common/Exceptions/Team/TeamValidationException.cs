using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Team;

public class TeamValidationException : BadRequestException
{
    public TeamValidationException(string message) : base(message)
    {
    }
}