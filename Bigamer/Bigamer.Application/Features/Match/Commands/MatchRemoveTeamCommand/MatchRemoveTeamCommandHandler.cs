using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchRemoveTeamCommand;

public class MatchRemoveTeamCommandHandler : IRequestHandler<MatchRemoveTeamCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchRemoveTeamCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchRemoveTeamCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchTeams)
            .Include(i => i.Teams)
            .ThenInclude(i => i.TeamInfo)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        var matchTeamToRemove = matchFromDb.MatchTeams
            .FirstOrDefault(i => i.TeamId == props.TeamId);

        if (matchFromDb.FinishDate is not null)
            throw new MatchBadRequestException("Match is already finished! You cant remove teams");

        if (matchTeamToRemove is null)
            throw new MatchBadRequestException("Team not found");
        
        matchTeamToRemove.Team.TeamInfo.GamesCount--;
        matchFromDb.MatchTeams.Remove(matchTeamToRemove);

        if (!matchFromDb.MatchTeams.Any())
            throw new MatchBadRequestException("At least one team should participate!");
            
        var maxScore = matchFromDb.MatchTeams
            .Max(i => i.TeamResult);

        var matchTopScore = matchFromDb.MatchTeams
            .Where(i => i.TeamResult == maxScore)
            .ToList();

        var matchTopScoreCount = matchTopScore.Count;
        
        switch (matchTopScore.Count)
        {
            case > 1:
            {
                foreach (var matchTeam in matchTopScore)
                {
                    matchTeam.TakenPlace = 2;
                }

                if (matchFromDb.FinishDate is not null)
                {
                    foreach (var matchTeam in matchTopScore)
                    {
                        matchTeam.Team.TeamInfo.DrawsCount += 1;
                    }
                }
                
                break;
            }
            case 1:
            {
                var matchTeam = matchTopScore.First();
                matchTeam.TakenPlace = 1;
                
                if (matchFromDb.FinishDate is not null)
                {
                    matchTeam.Team.TeamInfo.WinsCount += 1;
                }
                
                break;
            }
        }

        var otherMatchTeams = matchFromDb.MatchTeams
            .Except(matchTopScore)
            .ToList();

        var placeCounter = matchTopScoreCount > 1
            ? 3
            : 2;
        
        foreach (var matchTeam in otherMatchTeams)
        {
            matchTeam.TakenPlace = placeCounter++;

            if (matchFromDb.FinishDate is not null)
                matchTeam.Team.TeamInfo.LosesCount += 1;
        }

        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}