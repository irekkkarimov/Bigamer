using System.Text;

namespace Bigamer.Domain.Contracts;

public static class EmailModel
{
    public static string GetConfirmationEmailModel(string email, string code)
    {
        var htmlBuilder = new StringBuilder();

        htmlBuilder.Append("<div style=\"display: flex; flex-direction: column; align-items: center;\">");
        htmlBuilder.Append("<p>Confirm your email:</p>");
        htmlBuilder.Append($"<a href=\"http://localhost:5002/subscription/confirm?" +
                           $"email={email}&confirmationCode={code}\">Click to confirm</a>");
        htmlBuilder.Append("</div>");

        return htmlBuilder.ToString();
    }
}