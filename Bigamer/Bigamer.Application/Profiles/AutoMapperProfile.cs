using AutoMapper;
using Bigamer.Application.DTOs.Team.Queries.TeamGetAllRequest;
using Bigamer.Application.DTOs.Team.Queries.TeamGetRequest;
using Bigamer.Domain.Entities;

namespace Bigamer.Application.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Team, TeamGetAllResponseItem>()
            .ForMember(i => i.TeamId,
                i =>
                    i.MapFrom(e => e.Id))
            .ForMember(i => i.Name,
                i =>
                    i.MapFrom(e => e.Name))
            .ForMember(i => i.ImageUrl,
                i =>
                    i.MapFrom(e => e.TeamInfo.ImageUrl));

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
    }
}