using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Link;

public class BadLinkException : BadRequestException
{
    public BadLinkException(string message) : base(message)
    {
    }
}