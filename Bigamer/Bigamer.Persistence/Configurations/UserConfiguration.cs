using Bigamer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bigamer.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(i => i.Subscriber)
            .WithOne(i => i.User)
            .HasForeignKey<Subscriber>(i => i.UserId)
            .IsRequired(false);
    }
}