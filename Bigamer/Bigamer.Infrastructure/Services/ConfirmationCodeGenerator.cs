using System.Text;
using Bigamer.Application.Interfaces;

namespace Bigamer.Infrastructure.Services;

public class ConfirmationCodeGenerator : IConfirmationCodeGenerator
{
    private const string AllowedCharacters = "qwertyuiopasdfghjklzxcvbnm1234567890";
    
    public string GenerateCode(int length)
    {
        var r = new Random();
        var randomMax = AllowedCharacters.Length;
        var result = new StringBuilder();

        for (var i = 0; i < length; i++)
            result.Append(AllowedCharacters[r.Next(0, randomMax)]);

        return result.ToString();
    }
}