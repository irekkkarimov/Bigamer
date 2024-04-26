using System.Net;

namespace Bigamer.Application.Common.Exceptions.Abstractions;

public class BadRequestException : ApplicationBaseException
{
    public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
    {
    }
}