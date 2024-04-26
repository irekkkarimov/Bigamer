using System.Net;

namespace Bigamer.Application.Common.Exceptions.Abstractions;

public class InternalException : ApplicationBaseException
{
    public InternalException(string message) : base(HttpStatusCode.InternalServerError, message)
    {
    }
}