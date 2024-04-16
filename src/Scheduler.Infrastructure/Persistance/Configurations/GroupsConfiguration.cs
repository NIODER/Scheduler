using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.FinancialPlanAggregate.ValueObjects;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.GroupAggregate.ValueObjects;
using Scheduler.Domain.TaskAggregate.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Configurations;

public sealed class GroupsConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        ConfigureProperties(builder);
        ConfigureGroupUsers(builder);
        ConfigureGroupInvites(builder);
        ConfigureTaskIds(builder);
        ConfigureFinancialPlanIds(builder);
    }

    private static void ConfigureProperties(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(g => g.GroupName)
            .HasMaxLength(120)
            .IsRequired();
    }

    private static void ConfigureGroupUsers(EntityTypeBuilder<Group> builder)
    {
        builder.OwnsMany(g => g.Users, groupUserBuilder =>
        {
            groupUserBuilder.HasKey(gu => new { gu.UserId, gu.GroupId });
            groupUserBuilder.WithOwner().HasForeignKey(nameof(GroupId));
            groupUserBuilder.Property(ug => ug.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();

            groupUserBuilder.Property(ug => ug.GroupId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();
            
            groupUserBuilder.Property(ug => ug.Permissions)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(Group.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureGroupInvites(EntityTypeBuilder<Group> builder)
    {
        builder.OwnsMany(g => g.Invites, invitesBuilder =>
        {
            invitesBuilder.HasKey(gi => gi.Id);
            invitesBuilder.Property(gi => gi.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();
            
            invitesBuilder.Property(gi => gi.Permissions)
                .IsRequired();

            invitesBuilder.Property(gi => gi.Expire);
            
            invitesBuilder.Property(gi => gi.Message)
                .HasMaxLength(500);
            
            invitesBuilder.Property(gi => gi.Usages);
            
            invitesBuilder.Property(gi => gi.CreatorId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();

            invitesBuilder.Property(gi => gi.GroupId)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                )
                .ValueGeneratedNever();

            invitesBuilder.WithOwner().HasForeignKey(i => i.GroupId);
        });

        builder.Metadata.FindNavigation(nameof(Group.Invites))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureTaskIds(EntityTypeBuilder<Group> builder)
    {
        builder.OwnsMany(g => g.TaskIds, tidBuilder => 
        {
            tidBuilder.WithOwner().HasForeignKey(nameof(GroupId));
            tidBuilder.HasKey("Id");
            tidBuilder.Property(tid => tid.Value)
                .HasColumnName(nameof(TaskId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Group.TaskIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureFinancialPlanIds(EntityTypeBuilder<Group> builder)
    {
        builder.OwnsMany(g => g.FinancialPlanIds, fpidBuilder =>
        {
            fpidBuilder.WithOwner().HasForeignKey(nameof(GroupId));
            fpidBuilder.HasKey("Id");
            fpidBuilder.Property(fpid => fpid.Value)
                .HasColumnName(nameof(FinancialPlanId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Group.FinancialPlanIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
