using System.Net;

namespace Bigamer.Application.Common.Exceptions.Abstractions;

public abstract class ApplicationBaseException : Exception
{
    public ApplicationBaseException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
}