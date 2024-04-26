using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.User;

public class UserValidationException : BadRequestException
{
    public UserValidationException(string message) : base(message)
    {
    }
}