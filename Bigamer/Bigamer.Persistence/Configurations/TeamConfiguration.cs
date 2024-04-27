using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasOne(i => i.Game)
            .WithMany(i => i.Teams)
            .HasForeignKey(i => i.GameId);

        builder.HasMany(i => i.Matches)
            .WithMany(i => i.Teams)
            .UsingEntity<MatchTeam>();

        builder.HasMany(i => i.TeamLinks)
            .WithOne(i => i.Team)
            .HasForeignKey(i => i.TeamId);
    }
}