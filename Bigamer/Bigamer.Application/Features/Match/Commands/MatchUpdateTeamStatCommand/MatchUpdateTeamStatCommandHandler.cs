using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchUpdateTeamStatCommand;

public class MatchUpdateTeamStatCommandHandler : IRequestHandler<MatchUpdateTeamStatCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchUpdateTeamStatCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchUpdateTeamStatCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.MatchTeams)
            .ThenInclude(i => i.Team)
            .ThenInclude(i => i.TeamInfo)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        var teamToUpdate = matchFromDb.MatchTeams
            .FirstOrDefault(i => i.TeamId == props.TeamId);

        if (teamToUpdate is null)
            throw new MatchBadRequestException("Team not found");

        var maxScore = matchFromDb.MatchTeams
            .Max(i => i.TeamResult);
            
        var matchTopScore = matchFromDb.MatchTeams
            .Where(i => i.TeamResult == maxScore)
            .ToList();
        
        if (matchFromDb.FinishDate is not null)
        {
            switch (matchTopScore.Count)
            {
                case > 1:
                {
                    foreach (var matchTeam in matchTopScore
                                 .Where(matchTeam => matchTeam.Team.TeamInfo.DrawsCount > 0))
                    {
                        matchTeam.Team.TeamInfo.DrawsCount -= 1;
                    }

                    break;
                }
                case 1:
                {
                    var matchTeam = matchTopScore.First();

                    if (matchTeam.Team.TeamInfo.WinsCount > 0)
                        matchTeam.Team.TeamInfo.WinsCount -= 1;
                    
                    break;
                }
            }

            var otherTeams = matchFromDb.MatchTeams
                .Except(matchTopScore);

            foreach (var matchTeam in otherTeams
                         .Where(matchTeam => matchTeam.Team.TeamInfo.LosesCount > 0))
            {
                matchTeam.Team.TeamInfo.LosesCount -= 1;
            }
        }
        
        teamToUpdate.TeamResult = props.Score;
        maxScore = matchFromDb.MatchTeams
            .Max(i => i.TeamResult);

        matchTopScore = matchFromDb.MatchTeams
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