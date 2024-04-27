using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Link;

public class BadLinkServiceException : BadRequestException
{
    public BadLinkServiceException(string message) : base(message)
    {
    }
}