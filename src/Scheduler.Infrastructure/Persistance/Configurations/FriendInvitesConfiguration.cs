using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.UserAggregate.Entities;

namespace Scheduler.Infrastructure.Persistance.Configurations;

public class FriendInvitesConfiguration : IEntityTypeConfiguration<FriendsInvite>
{
    public void Configure(EntityTypeBuilder<FriendsInvite> builder)
    {
        builder.ToTable(nameof(SchedulerDbContext.FriendsInvites));
        builder.HasKey(fi => fi.Id);
        builder.Property(fi => fi.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(fi => fi.SenderId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
            
        builder.Property(fi => fi.AddressieId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(fi => fi.Message)
            .HasMaxLength(500);
    }
}
