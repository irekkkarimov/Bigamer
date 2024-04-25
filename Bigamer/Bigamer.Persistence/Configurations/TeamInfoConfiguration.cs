using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class TeamInfoConfiguration : IEntityTypeConfiguration<TeamInfo>
{
    public void Configure(EntityTypeBuilder<TeamInfo> builder)
    {
        builder.Ignore(i => i.Id);

        builder.HasKey(i => i.TeamId);

        builder.HasOne(i => i.Team)
            .WithOne(i => i.TeamInfo)
            .HasForeignKey<TeamInfo>(i => i.TeamId);
    }
}