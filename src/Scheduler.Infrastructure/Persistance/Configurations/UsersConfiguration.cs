using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.ProblemAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Configurations;

public sealed class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureProperties(builder);
        ConfigureGroupIds(builder);
        ConfigureFinancialPlanIds(builder);
        ConfigureFriendsInvites(builder);
        ConfigureTaskIds(builder);
    }

    private static void ConfigureProperties(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(u => u.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Description)
            .HasMaxLength(1000);
        
        builder.Property(u => u.PasswordHash)
            .HasMaxLength(64)
            .IsRequired();
    }

    private static void ConfigureGroupIds(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.GroupIds, gidBuilder =>
        {
            gidBuilder.WithOwner().HasForeignKey(nameof(UserId));
            gidBuilder.HasKey(gid => gid.Value);
            gidBuilder.Property(gid => gid.Value)
                .HasColumnName(nameof(GroupId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.GroupIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureFinancialPlanIds(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.FinancialPlanIds, financialPlanIdsBuilder =>
        {
            financialPlanIdsBuilder.WithOwner().HasForeignKey(nameof(UserId));
            financialPlanIdsBuilder.HasKey(fpid => fpid.Value);
            financialPlanIdsBuilder.Property(fpid => fpid.Value)
                .HasColumnName(nameof(FinancialPlanId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.FinancialPlanIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureFriendsInvites(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.FriendsInvites, friendsInvitesBuilder =>
        {
            friendsInvitesBuilder.HasKey(fi => fi.Id);
            friendsInvitesBuilder.Property(fi => fi.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();

            friendsInvitesBuilder.Property(fi => fi.SenderId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();
            
            friendsInvitesBuilder.Property(fi => fi.AddressieId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();
            
            friendsInvitesBuilder.HasOne<User>()
                .WithMany()
                .HasForeignKey(fi => fi.SenderId);
            
            friendsInvitesBuilder.HasOne<User>()
                .WithMany()
                .HasForeignKey(fi => fi.AddressieId);

            friendsInvitesBuilder.Property(fi => fi.Message)
                .HasMaxLength(500);
        });

        builder.Metadata.FindNavigation(nameof(User.FriendsInvites))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureTaskIds(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.ProblemIds, tidBuilder =>
        {
            tidBuilder.WithOwner().HasForeignKey(nameof(UserId));
            tidBuilder.HasKey(tid => tid.Value);
            tidBuilder.Property(tid => tid.Value)
                .HasColumnName(nameof(ProblemId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.ProblemIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
