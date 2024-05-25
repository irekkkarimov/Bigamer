using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Contracts;
using Bigamer.Domain.Entities;
using Bigamer.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchAddCommand;

public class MatchAddCommandHandler : IRequestHandler<MatchAddCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;

    public MatchAddCommandHandler(IApplicationDbContext dbContext, IEmailSender emailSender)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
    }

    public async Task Handle(MatchAddCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        var gameFromDb = await _dbContext.Games
            .FirstOrDefaultAsync(i => i.Id == props.GameId, cancellationToken);
        
        if (gameFromDb is null)
            throw new MatchBadRequestException("Game not found");

        if (props.Teams.Count > gameFromDb.MaxTeamCount)
            throw new MatchBadRequestException(
                $"{gameFromDb.Name} Match can only have {gameFromDb.MaxTeamCount} teams!");
        
        if (props.StartDate is null)
            throw new MatchBadRequestException("Wrong start date");
        
        if (props.FinishDate is not null && props.FinishDate.Value > DateTime.UtcNow)
            throw new MatchBadRequestException("You cant set match finish date to future");
        
        var newMatch = new Domain.Entities.Match
        {
            Id = Guid.NewGuid(),
            StartDate = props.StartDate?.ToUniversalTime(),
            FinishDate = props.FinishDate?.ToUniversalTime(),
            Game = gameFromDb
        };

        var newMatchInfo = new MatchInfo
        {
            Id = Guid.NewGuid(),
            Match = newMatch,
            Stage = props.Stage,
            Prize = props.Prize,
            Other = ""
        };

        var newMatchLinks = props.Links
            .Select(i => new MatchLink
            {
                Id = Guid.NewGuid(),
                Match = newMatch,
                ServiceName = i.ServiceName,
                Link = i.Link
            })
            .ToList();

        var teamsFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .Where(i => props.Teams.Select(e => e.TeamId).Contains(i.Id))
            .ToListAsync(cancellationToken);

        if (teamsFromDb.Any(i => i.GameId != gameFromDb.Id))
            throw new MatchBadRequestException($"Team is not a {gameFromDb.Name} team!");
        
        foreach (var team in teamsFromDb)
            team.TeamInfo.GamesCount++;
        
        newMatch.MatchInfo = newMatchInfo;
        newMatch.MatchLinks = newMatchLinks;
        newMatch.Teams = teamsFromDb;
        
        await _dbContext.Matches.AddAsync(newMatch, cancellationToken);
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
        
        foreach (var subscriber in await _dbContext.Subscribers
                     .ToListAsync(cancellationToken))
        {
            await _emailSender.SendEmailAsync(subscriber.Email, EmailModel.GetAddMatchModel(gameFromDb.Name));
        }
    }
}