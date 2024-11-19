using Scheduler.Domain.Common.Schedule;
using System.ComponentModel;

namespace Scheduler.Domain.FinancialPlanAggregate.Calculation.Dates
{
    public static class DatesActualizerFabfic
    {
        public static IDatesActualizer GetDatesActualizerByRepeatType(RepeatType repeatType)
        {
            return repeatType switch
            {
                RepeatType.None => throw new InvalidEnumArgumentException(nameof(repeatType), (int)repeatType, typeof(RepeatType)),
                RepeatType.Days => new DaysDatesActualizer(),
                RepeatType.Weeks => new WeekDatesActualizer(),
                RepeatType.Months => throw new NotImplementedException(),
                RepeatType.Years => throw new NotImplementedException(),
                RepeatType.LastDayOfMonth => throw new NotImplementedException(),
                RepeatType.LastDayOfYear => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
