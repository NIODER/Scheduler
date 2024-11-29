using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Scheduling.ValueObjects;

namespace Scheduler.Infrastructure.Persistance.Configurations;

internal class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(s => s.Deadline)
                .IsRequired();

        builder.Property(s => s.ScheduledDate)
            .IsRequired();

        builder.Property(s => s.OriginScheduledDate)
            .IsRequired();

        builder.Property(s => s.ScheduleType)
            .IsRequired();
    }
}
