using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasMany(i => i.MatchLinks)
            .WithOne(i => i.Match)
            .HasForeignKey(i => i.MatchId);

        builder.HasOne(i => i.Game)
            .WithMany(i => i.Matches)
            .HasForeignKey(i => i.GameId);
    }
}