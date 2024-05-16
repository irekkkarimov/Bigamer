namespace Bigamer.Application.Interfaces;

public interface IConfirmationCodeGenerator
{
    string GenerateCode(int length);
}