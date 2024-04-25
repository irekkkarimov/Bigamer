using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class MatchInfoConfiguration : IEntityTypeConfiguration<MatchInfo>
{
    public void Configure(EntityTypeBuilder<MatchInfo> builder)
    {
        builder.Ignore(i => i.Id);

        builder.HasKey(i => i.MatchId);

        builder.HasOne(i => i.Match)
            .WithOne(i => i.MatchInfo)
            .HasForeignKey<MatchInfo>(i => i.MatchId);
    }
}