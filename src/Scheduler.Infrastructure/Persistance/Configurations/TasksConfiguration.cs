using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Scheduler.Domain.TaskAggregate.Task;

namespace Scheduler.Infrastructure.Persistance.Configurations;

public sealed class TasksConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        ConfigureProperties(builder);
    }

    private static void ConfigureProperties(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();

        builder.Property(t => t.CreatorId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(t => t.UserId)
            .HasConversion(
                id => id == null ? default : id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();
        
        builder.Property(t => t.GroupId)
            .HasConversion(
                id => id == null ? default : id.Value,
                value => new(value)
            )
            .ValueGeneratedNever();

        builder.Property(t => t.Title)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(1000);
        
        builder.Property(t => t.Status)
            .IsRequired();
        
        builder.Property(t => t.Deadline)
            .IsRequired();
    }
}
