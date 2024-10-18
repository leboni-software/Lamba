namespace Lamba.Common.Models.Results
{
    public sealed record SuccessResult(string Message) : Result(true, Message);
    public sealed record SuccessResult<T>(T? Data, string Message) : Result<T>(Data, true, Message);
}
