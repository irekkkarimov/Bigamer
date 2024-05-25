using Bigamer.Application.Common.Exceptions.Match;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Features.Match.Commands.MatchAddTeamCommand;

public class MatchAddTeamCommandHandler : IRequestHandler<MatchAddTeamCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public MatchAddTeamCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(MatchAddTeamCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;

        var matchFromDb = await _dbContext.Matches
            .Include(i => i.Game)
            .Include(i => i.Teams)
            .Include(i => i.MatchTeams)
            .FirstOrDefaultAsync(i => i.Id == props.MatchId, cancellationToken);

        if (matchFromDb is null)
            throw new MatchBadRequestException("Match not found");

        if (matchFromDb.Teams.Count + 1 > matchFromDb.Game.MaxTeamCount)
            throw new MatchBadRequestException(
                $"{matchFromDb.Game.Name} Match can only have {matchFromDb.Game.MaxTeamCount} teams!");
        
        if (matchFromDb.FinishDate is not null)
            throw new MatchBadRequestException("Match is already finished! You cant add teams!");
        
        if (matchFromDb.Teams.Select(i => i.Id).Contains(props.TeamId))
            throw new MatchBadRequestException("Team is already participating in this match");
        
        var teamFromDb = await _dbContext.Teams
            .Include(i => i.TeamInfo)
            .FirstOrDefaultAsync(i => i.Id == props.TeamId, cancellationToken);
        
        if (teamFromDb is null)
            throw new MatchBadRequestException("Team not found");

        if (teamFromDb.GameId != matchFromDb.GameId)
            throw new MatchBadRequestException($"Team is not a {matchFromDb.Game.Name} team!");
        
        matchFromDb.MatchTeams.Add(new MatchTeam
        {
            Match = matchFromDb,
            Team = teamFromDb,
            TeamResult = 0,
            TakenPlace = 0
        });
        
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
        
        teamFromDb.TeamInfo.GamesCount++;
        await _dbContext.Context.SaveChangesAsync(cancellationToken);
    }
}