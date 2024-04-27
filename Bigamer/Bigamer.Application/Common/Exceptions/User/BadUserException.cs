using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.User;

public class BadUserException : BadRequestException
{
    public BadUserException(string message) : base(message)
    {
    }
}