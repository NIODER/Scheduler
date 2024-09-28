namespace Scheduler.Application.Common.Wrappers;

internal class Result<T>
{
    public bool HasError { get; set; }
    public T? Value { get; set; }
    public IErrorResult<T>? Error { get; set; }

    private Result(bool hasError, T? value, IErrorResult<T>? error)
    {
        HasError = hasError;
        Value = value;
        Error = error;
    }

    public static Result<T> FromError(IErrorResult<T> error)
    {
        return new Result<T>(true, default, error);
    }

    public static Result<T> FromSuccess(T? result = default)
    {
        return new(false, result, null);
    }

    public SuccessResult<T> ToSuccessResult()
    {
        return new SuccessResult<T>(Value);
    }
}
