namespace Scheduler.Domain.FinancialPlanAggregate.Calculation.Dates;

public interface IDatesActualizer
{
    /// <summary>
    /// Actualizing given dates by the <paramref name="origin"/> date. In result <paramref name="origin"/> must be between <paramref name="scheduledDate"/> and <paramref name="expirationDate"/> 
    /// and <paramref name="scheduledDate"/> must be less or equal to <paramref name="origin"/> and <paramref name="expirationDate"/> must be greater than <paramref name="origin"/>.
    /// </summary>
    /// <param name="origin">Date that need to be between <paramref name="scheduledDate"/> and <paramref name="expirationDate"/></param>
    /// <param name="scheduledDate">First date of a period. First expiration date after creation.</param>
    /// <param name="expirationDate">Second expiration date after <paramref name="scheduledDate"/>. End of period.</param>
    /// <returns><see langword="true"/> if <paramref name="origin"/> already lies in perion between <paramref name="scheduledDate"/> and <paramref name="expirationDate"/></returns>
    bool ActualizeDates(DateTime origin, ref DateTime scheduledDate, ref DateTime expirationDate);
}
