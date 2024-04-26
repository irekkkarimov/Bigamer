using System.Net;

namespace Bigamer.Application.Common.Exceptions.Abstractions;

public class NotFoundException : ApplicationBaseException
{
    public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
    }
}