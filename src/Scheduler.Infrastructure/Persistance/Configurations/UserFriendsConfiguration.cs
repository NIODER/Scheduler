using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Configurations
{
    internal class UserFriendsConfiguration : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            ConfigureUserFriends(builder);
        }

        private void ConfigureUserFriends(EntityTypeBuilder<UserFriend> builder)
        {
            builder.HasKey(uf => new { uf.UserId1, uf.UserId2 });

            builder.Property(uf => uf.UserId1)
                .HasConversion(
                    id => id.Value,
                    value => new(value))
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(uf => uf.UserId2)
                .HasConversion(
                    id => id.Value,
                    value => new(value))
                .ValueGeneratedNever()
                .IsRequired();

            builder.HasOne(uf => uf.User1)
                .WithMany(u => u.ReceivedUserFriends)
                .HasForeignKey(uf => uf.UserId1);
            builder.HasOne(uf => uf.User2)
                .WithMany(u => u.InitiatedUserFriends)
                .HasForeignKey(uf => uf.UserId2);
        }
    }
}
