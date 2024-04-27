using System.Reflection;
using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bigamer.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbContext Context => this;
    public DbSet<UserInfo> UserInfos { get; set; } = null!;
    public DbSet<Subscriber> Subscribers { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<TeamInfo> TeamInfos { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<MatchInfo> MatchInfos { get; set; } = null!;
    public DbSet<MatchLink> MatchLinks { get; set; } = null!;
    public DbSet<TeamLink> TeamLinks { get; set; }
}