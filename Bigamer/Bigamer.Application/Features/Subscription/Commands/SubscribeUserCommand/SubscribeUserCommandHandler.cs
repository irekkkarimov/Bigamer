using Bigamer.Application.Interfaces;
using Bigamer.Domain.Contracts;
using Bigamer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Subscription.Commands.SubscribeUserCommand;

public class SubscribeUserCommandHandler : IRequestHandler<SubscribeUserCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;
    private readonly IConfirmationCodeGenerator _confirmationCodeGenerator;

    public SubscribeUserCommandHandler(IApplicationDbContext dbContext, IEmailSender emailSender,
        IConfirmationCodeGenerator confirmationCodeGenerator)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
        _confirmationCodeGenerator = confirmationCodeGenerator;
    }

    public async Task Handle(SubscribeUserCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        var isNew = false;

        if (string.IsNullOrWhiteSpace(props.Email))
            throw new ArgumentException("Wrong email!");

        if (string.IsNullOrWhiteSpace(props.Name))
            throw new ArgumentException("Wrong name!");

        var subscriberFromDb = await _dbContext.Subscribers
            .FirstOrDefaultAsync(i => i.Email.Equals(props.Email), cancellationToken);

        if (subscriberFromDb is not null)
        {
            if (string.IsNullOrWhiteSpace(subscriberFromDb.ConfirmationCode))
                throw new ArgumentException("User is already subscribed!");

            if (DateTime.UtcNow - subscriberFromDb.LastSubscribeAttempt < TimeSpan.FromDays(1))
                throw new ArgumentException("Wait one day for another attempt!");
        }

        if (subscriberFromDb is null)
        {
            subscriberFromDb = new Subscriber
            {
                Id = Guid.NewGuid(),
                Name = props.Name,
                Email = props.Email
            };
            isNew = true;
        }

        var newConfirmationCode = _confirmationCodeGenerator.GenerateCode(6);

        subscriberFromDb.ConfirmationCode = newConfirmationCode;
        var message = EmailModel.GetConfirmationEmailModel(props.Email, newConfirmationCode);

        subscriberFromDb.LastSubscribeAttempt = DateTime.UtcNow;

        if (isNew)
            await _dbContext.Subscribers.AddAsync(subscriberFromDb, cancellationToken);

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
        await _emailSender.SendEmailAsync(props.Email, message);
    }
}