using System.Text.Json;
using Bigamer.Application.Interfaces;
using Bigamer.Infrastructure.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Bigamer.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderConfiguration _configuration;

    public EmailSender(EmailSenderConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string message)
    {
        Console.WriteLine(JsonSerializer.Serialize(_configuration));
        using var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_configuration.FromName, _configuration.EmailAddress));
        emailMessage.To.Add(new MailboxAddress("", email));

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = message
        };
        var body = bodyBuilder.ToMessageBody();
        emailMessage.Body = body;

        using var client = new SmtpClient();

        await client.ConnectAsync(_configuration.SMTPServerHost, _configuration.SMTPServerPort);
        await client.AuthenticateAsync(_configuration.EmailAddress, _configuration.Password);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}