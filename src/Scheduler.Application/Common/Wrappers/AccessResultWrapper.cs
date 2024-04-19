namespace Scheduler.Application.Common.Wrappers;

public sealed class AccessResultWrapper<T> where T : class
{
    private readonly T? _result;
    public bool IsForbidden { get; private set; }
    public string? ClientMessage { get; private set; }
    public T Result => _result ?? throw new NullReferenceException("Result was null");

    private AccessResultWrapper(
        bool forbidden,
        string? clientMessage,
        T? result)
    {
        IsForbidden = forbidden;
        ClientMessage = clientMessage;
        _result = result;
    }

    public static AccessResultWrapper<T> CreateForbidden(
        T? result = null,
        string? clientMessage = null) => new(
            true,
            clientMessage,
            result
        );

    public static AccessResultWrapper<T> Create(T result) => new(
            false,
            null,
            result
        );
}