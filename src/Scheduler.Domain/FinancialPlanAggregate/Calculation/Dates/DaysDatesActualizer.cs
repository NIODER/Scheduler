namespace Scheduler.Domain.FinancialPlanAggregate.Calculation.Dates;

/// <summary>
/// DateCalculator for <see cref="Scheduler.Domain.Common.Schedule.RepeatType.Days"/>
/// </summary>
internal class DaysDatesActualizer : IDatesActualizer
{
    public bool ActualizeDates(DateTime origin, ref DateTime scheduledDate, ref DateTime expirationDate)
    {
        if (scheduledDate <= origin && expirationDate >= origin)
        {
            return true;
        }

        int daysCount = (expirationDate - scheduledDate).Days;

        if (expirationDate < origin)
        {
            while (expirationDate < origin)
            {
                expirationDate = expirationDate.AddDays(daysCount);
            }

            scheduledDate = expirationDate.AddDays(-daysCount);
        }
        else
        {
            while (scheduledDate > origin)
            {
                scheduledDate = scheduledDate.AddDays(-daysCount);
            }

            expirationDate = scheduledDate.AddDays(daysCount);
        }

        return false;
    }
}
