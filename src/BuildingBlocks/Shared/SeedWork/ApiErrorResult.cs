namespace Shared.SeedWork;

public class ApiErrorResult<T> : ApiResult<T>
{
    public List<string> Errors { set; get; }
    public ApiErrorResult() : this(message: "Something wrong happened. Please try again later")
    {
    }

    public ApiErrorResult(string message)
        : base(isSucceeded: false, message)
    {
    }

    public ApiErrorResult(List<string> errors)
        : base(isSucceeded: false)
    {
        Errors = errors;
    }

}
