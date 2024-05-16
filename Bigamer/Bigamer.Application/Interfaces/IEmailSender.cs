namespace Bigamer.Application.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string message);
}