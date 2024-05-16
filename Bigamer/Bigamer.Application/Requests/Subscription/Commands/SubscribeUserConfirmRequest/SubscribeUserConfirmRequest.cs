namespace Bigamer.Application.Requests.Subscription.Commands.SubscribeUserConfirmRequest;

public class SubscribeUserConfirmRequest
{
    public string Email { get; set; } = null!;
    public string ConfirmationCode { get; set; } = null!;
}