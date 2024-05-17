using AutoMapper;
using Bigamer.Application.DTOs.Links;
using Bigamer.Application.Requests.Match.Queries.MatchGetAllRequest;
using Bigamer.Application.Requests.Match.Queries.MatchGetRandomActiveRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetAllRequest;
using Bigamer.Application.Requests.Team.Queries.TeamGetRequest;
using Bigamer.Application.Requests.User.Queries.UserGetAllRequest;
using Bigamer.Domain.Entities;
using Bigamer.Shared.Enums;

namespace Bigamer.Application.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // TEAM GET ALL
        CreateMap<Team, TeamGetAllResponseItem>()
            .ForMember(i => i.TeamId,
                i =>
                    i.MapFrom(e => e.Id))
            .ForMember(i => i.Name,
                i =>
                    i.MapFrom(e => e.Name))
            .ForMember(i => i.ImageUrl,
                i =>
                    i.MapFrom(e => e.TeamInfo.ImageUrl))
            .ForMember(i => i.Links,
                i =>
                    i.MapFrom(e => e.TeamLinks));

        
        // TEAM GET SINGLE
        CreateMap<Team, TeamGetResponse>()
            .ForMember(i => i.Name,
                i =>
                    i.MapFrom(e => e.Name))
            .ForMember(i => i.WinsCount,
                i =>
                    i.MapFrom(e => e.TeamInfo.WinsCount))
            .ForMember(i => i.LosesCount,
                i =>
                    i.MapFrom(e => e.TeamInfo.LosesCount))
            .ForMember(i => i.DrawsCount,
                i =>
                    i.MapFrom(e => e.TeamInfo.DrawsCount))
            .ForMember(i => i.GamesCount,
                i =>
                    i.MapFrom(e => e.TeamInfo.GamesCount))
            .ForMember(i => i.ImageUrl,
                i =>
                    i.MapFrom(e => e.TeamInfo.ImageUrl))
            .ForMember(i => i.About,
                i =>
                    i.MapFrom(e => e.TeamInfo.About))
            .ForMember(i => i.Game,
                i =>
                    i.MapFrom(e => e.Game))
            .ForMember(i => i.Players,
                i =>
                    i.MapFrom(e => e.UserInfos));

        CreateMap<UserInfo, TeamGetResponsePlayer>()
            .ForMember(i => i.FirstName,
                i =>
                    i.MapFrom(e => e.FirstName))
            .ForMember(i => i.LastName,
                i =>
                    i.MapFrom(e => e.LastName))
            .ForMember(i => i.Nickname,
                i =>
                    i.MapFrom(e => e.NickName))
            .ForMember(i => i.ImageUrl,
                i =>
                    i.MapFrom(e => e.ImageUrl));

        CreateMap<Game, TeamGetResponseGame>()
            .ForMember(i => i.Name,
                i =>
                    i.MapFrom(e => e.Name));

        CreateMap<TeamLink, GetLink>()
            .ForMember(i => i.Link,
                i =>
                    i.MapFrom(e => e.Link))
            .ForMember(i => i.ServiceName,
                i =>
                    i.MapFrom(e => e.ServiceName));


        // MATCH GET ALL
        CreateMap<Match, MatchGetAllResponseItem>()
            .ForMember(i => i.Id,
                i => 
                    i.MapFrom(e => e.Id))
            .ForMember(i => i.GameName,
                i =>
                    i.MapFrom(e => e.Game.Name))
            .ForMember(i => i.StartDate,
                i =>
                    i.MapFrom(e => e.StartDate))
            .ForMember(i => i.FinishDate,
                i =>
                    i.MapFrom(e => e.FinishDate))
            .ForMember(i => i.Prize,
                i =>
                    i.MapFrom(e => e.MatchInfo.Prize))
            .ForMember(i => i.Teams,
                i =>
                    i.MapFrom(e => e.Teams))
            .ForMember(i => i.Links,
                i =>
                    i.MapFrom(e => e.MatchLinks));

        CreateMap<MatchLink, GetLink>()
            .ForMember(i => i.ServiceName,
                i =>
                    i.MapFrom(e => e.ServiceName))
            .ForMember(i => i.Link,
                i =>
                    i.MapFrom(e => e.Link));

        CreateMap<Team, MatchGetAllResponseTeam>()
            .ForMember(i => i.TeamId,
                i =>
                    i.MapFrom(e => e.Id))
            .ForMember(i => i.ImageUrl,
                i =>
                    i.MapFrom(e => e.TeamInfo.ImageUrl))
            .ForMember(i => i.TeamMatches,
                i => 
                    i.MapFrom(e => e.MatchTeams));

        CreateMap<MatchTeam, MatchGetAllResponseTeamMatch>()
            .ForMember(i => i.MatchId,
                i =>
                    i.MapFrom(e => e.MatchId))
            .ForMember(i => i.TeamResult,
                i =>
                    i.MapFrom(e => e.TeamResult));
        
        
        // USER GET
        CreateMap<User, UserGetAllResponseItem>()
            .ForMember(i => i.FirstName,
                i =>
                    i.MapFrom(e => e.UserInfo.FirstName))
            .ForMember(i => i.LastName,
                i =>
                    i.MapFrom(e => e.UserInfo.LastName))
            .ForMember(i => i.Username,
                i =>
                    i.MapFrom(e => e.UserName))
            .ForMember(i => i.TeamName,
                i =>
                    i.MapFrom(e => e.UserInfo.Team != null ? e.UserInfo.Team.Name : null))
            .ForMember(i => i.IsBanned,
                i =>
                    i.MapFrom(e => e.UserInfo.IsBanned));
    }
}