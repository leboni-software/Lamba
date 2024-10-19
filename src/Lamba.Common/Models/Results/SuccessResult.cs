using Lamba.Common.Constants;

namespace Lamba.Common.Models.Results
{
    public sealed record SuccessResult(string Message = ResultMessages.SuccessfulOperation) : Result(true, Message);
    public sealed record SuccessResult<T>(T? Data, string Message = ResultMessages.SuccessfulOperation) : Result<T>(Data, true, Message);
}
