using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
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
        ConfigureFriendsIds(builder);
        ConfigureBlacklist(builder);
    }

    private static void ConfigureProperties(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
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

        builder.Property(u => u.Settings)
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
        builder.OwnsMany(u => u.FriendsInviteIds, b =>
        {
            b.WithOwner().HasForeignKey(nameof(UserId));
            b.HasKey(fi => fi.Value);
            b.Property(fi => fi.Value)
                .HasColumnName(nameof(FriendsInviteId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.FriendsInviteIds))!
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

    private static void ConfigureFriendsIds(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.FriendsIds, fidBuilder =>
        {
            fidBuilder.WithOwner().HasForeignKey(nameof(UserId));
            fidBuilder.HasKey(fid => fid.Value);
            fidBuilder.Property(fid => fid.Value)
                .HasColumnName("FriendId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.FriendsIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureBlacklist(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.BlackListUserIds, blBuilder =>
        {
            blBuilder.WithOwner().HasForeignKey(nameof(UserId));
            blBuilder.HasKey(bl => bl.Value);
            blBuilder.Property(fid => fid.Value)
                .HasColumnName("BlockedUserId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.BlackListUserIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
