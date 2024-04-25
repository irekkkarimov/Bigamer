using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.Ignore(i => i.Id);

        builder.HasKey(i => i.UserId);

        builder.HasOne(i => i.User)
            .WithOne(i => i.UserInfo)
            .HasForeignKey<UserInfo>(i => i.UserId);

        builder.HasOne(i => i.Role)
            .WithMany(i => i.UserInfos)
            .HasForeignKey(i => i.RoleId);

        builder.HasOne(i => i.Team)
            .WithMany(i => i.UserInfos)
            .HasForeignKey(i => i.TeamId)
            .IsRequired(false);
    }
}