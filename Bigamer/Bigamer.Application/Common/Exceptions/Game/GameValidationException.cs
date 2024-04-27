using Bigamer.Application.Common.Exceptions.Abstractions;

namespace Bigamer.Application.Common.Exceptions.Game;

public class GameValidationException : BadRequestException
{
    public GameValidationException(string message) : base(message)
    {
    }
}