using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class MatchTeamConfiguration : IEntityTypeConfiguration<MatchTeam>
{
    public void Configure(EntityTypeBuilder<MatchTeam> builder)
    {
        builder.HasKey(i => new { i.MatchId, i.TeamId });

        builder.HasOne(i => i.Match)
            .WithMany(i => i.MatchTeams)
            .HasForeignKey(i => i.MatchId);

        builder.HasOne(i => i.Team)
            .WithMany(i => i.MatchTeams)
            .HasForeignKey(i => i.TeamId);
    }
}