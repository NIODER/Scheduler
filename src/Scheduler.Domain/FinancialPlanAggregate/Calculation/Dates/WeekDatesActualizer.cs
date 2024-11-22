namespace Scheduler.Domain.FinancialPlanAggregate.Calculation.Dates;

internal class WeekDatesActualizer : IDatesActualizer
{
    private const int DAYS_IN_WEEK = 7;

    public bool ActualizeDates(DateTime origin, ref DateTime scheduledDate, ref DateTime expirationDate)
    {
        int daysCount = (expirationDate - scheduledDate).Days;

        if (daysCount % DAYS_IN_WEEK != 0)
        {
            throw new InvalidDataException($"Can't describe offset between {nameof(scheduledDate)} and {nameof(expirationDate)} as integer number of weeks.");
        }

        if (scheduledDate <= origin && expirationDate >= origin)
        {
            return true;
        }

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
