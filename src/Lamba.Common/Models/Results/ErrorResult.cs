namespace Lamba.Common.Models.Results
{
    public sealed record ErrorResult(string Message) : Result(false, Message);
    public sealed record ErrorResult<T>(T? Data, string Message) : Result<T>(Data, false, Message);
}
