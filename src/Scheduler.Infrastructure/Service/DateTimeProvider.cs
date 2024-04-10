using Scheduler.Application.Common.Interfaces.Services;

namespace Scheduler.Infrastructure.Service;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
