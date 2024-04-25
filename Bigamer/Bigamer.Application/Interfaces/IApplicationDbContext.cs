using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Application.Interfaces;

public interface IApplicationDbContext : IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<UserInfo> UserInfos { get; set; }
    DbSet<Subscriber> Subscribers { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Team> Teams { get; set; }
    DbSet<TeamInfo> TeamInfos { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Match> Matches { get; set; }
    DbSet<MatchInfo> MatchInfos { get; set; }
    DbSet<MatchLink> MatchLinks { get; set; }
}