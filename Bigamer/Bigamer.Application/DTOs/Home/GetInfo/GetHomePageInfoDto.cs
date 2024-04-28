using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetRandomActiveRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;

namespace Bigamer.Application.DTOs.Home.GetInfo;

public class GetHomePageInfoDto
{
    public MatchGetRandomActiveResponse? TodayMatch { get; set; }
    public TeamGetAllResponse TeamsInfo { get; set; } = null!;
    public MatchGetAllResponse MatchesInfo { get; set; } = null!;
}