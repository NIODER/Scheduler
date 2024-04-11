namespace Scheduler.Domain.Common;

public abstract class ValueObject
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) => obj is ValueObject valueObject && 
        GetEqualityComponents()
        .SequenceEqual(valueObject.GetEqualityComponents());

    public override int GetHashCode() => GetEqualityComponents()
        .Select(x => x.GetHashCode())
        .Aggregate((x, y) => x ^ y);

    public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);
    public static bool operator !=(ValueObject left, ValueObject right) => !Equals(left, right);
}