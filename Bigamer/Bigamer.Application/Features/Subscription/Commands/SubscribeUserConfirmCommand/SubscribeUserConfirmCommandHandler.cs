using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Subscription.Commands.SubscribeUserConfirmCommand;

public class SubscribeUserConfirmCommandHandler : IRequestHandler<SubscribeUserConfirmCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;

    public SubscribeUserConfirmCommandHandler(IApplicationDbContext dbContext, IEmailSender emailSender)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
    }

    public async Task Handle(SubscribeUserConfirmCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        if (string.IsNullOrWhiteSpace(props.Email))
            throw new ArgumentException("Wrong email!");

        if (string.IsNullOrWhiteSpace(props.ConfirmationCode))
            throw new ArgumentException("Empty code!");

        var subscriberFromDb = await _dbContext.Subscribers
            .FirstOrDefaultAsync(i => i.Email.Equals(props.Email), cancellationToken);

        if (subscriberFromDb is null)
            throw new ArgumentException("Subscriber not found");

        if (string.IsNullOrWhiteSpace(subscriberFromDb.ConfirmationCode))
            throw new ArgumentException("User is already subscribed");

        if (!subscriberFromDb.ConfirmationCode.Equals(props.ConfirmationCode))
            throw new ArgumentException("Wrong confirmation code");

        subscriberFromDb.ConfirmationCode = null;
        subscriberFromDb.SubscribedAt = DateTime.UtcNow;
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}