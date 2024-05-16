namespace Bigamer.Infrastructure.Models;

public class EmailSenderConfiguration
{
    public string FromName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SMTPServerHost { get; set; } = null!;
    public int SMTPServerPort { get; set; }
}