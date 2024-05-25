using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchUpdateCommand;

public class MatchUpdateCommandHandler : IRequestHandler<MatchUpdateCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchUpdateCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchUpdateCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var startDate = props.StartDate?.ToUniversalTime();
        var finishDate = props.FinishDate?.ToUniversalTime();

        if (finishDate is not null && finishDate.Value > DateTime.UtcNow)
            throw new MatchBadRequestException("You cant set match finish date to future");
        
        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchInfo)
            .Include(i => i.MatchTeams)
            .ThenInclude(i => i.Team)
            .ThenInclude(i => i.TeamInfo)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        var wasFinishDateNull = matchFromDb.FinishDate is null;
        
        if (props.Prize is not null)
        {
            if (props.Prize.Value < 0)
                throw new MatchBadRequestException("Prize cant be a negative number");

            matchFromDb.MatchInfo.Prize = props.Prize.Value;
        }

        if (startDate is not null)
        {
            if (finishDate is null)
            {
                if (matchFromDb.FinishDate is not null)
                    if (matchFromDb.FinishDate.Value < startDate.Value)
                        throw new MatchBadRequestException("Start date should be earlier than Finish date");

                matchFromDb.StartDate = startDate;
            }
            else
            {
                if (finishDate.Value < startDate.Value)
                    throw new MatchBadRequestException("Start date should be earlier than Finish date");

                matchFromDb.StartDate = startDate.Value;
                matchFromDb.FinishDate = finishDate.Value;
            }
        }
        else
        {
            if (finishDate is not null)
            {
                if (matchFromDb.StartDate is not null)
                {
                    if (finishDate.Value < matchFromDb.StartDate.Value)
                        throw new MatchBadRequestException("Start date should be earlier than Finish date");

                    matchFromDb.FinishDate = finishDate;
                }
                else
                {
                    matchFromDb.FinishDate = finishDate.Value;
                }
            }
        }

        if (wasFinishDateNull && props.FinishDate is not null)
        {
            var secondPlaces = matchFromDb.MatchTeams
                .Where(i => i.TakenPlace == 2)
                .ToList();

            if (secondPlaces.Count > 1)
            {
                foreach (var matchTeam in secondPlaces)
                    matchTeam.Team.TeamInfo.DrawsCount++;
            }
            else
            {
                secondPlaces.First().Team.TeamInfo.LosesCount++;
            }

            var otherTeams = matchFromDb.MatchTeams
                .Except(secondPlaces);
            
            foreach (var matchTeam in otherTeams)
            {
                if (matchTeam.TakenPlace == 1)
                    matchTeam.Team.TeamInfo.WinsCount++;
                else
                    matchTeam.Team.TeamInfo.LosesCount++;
            }
        }
 
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}