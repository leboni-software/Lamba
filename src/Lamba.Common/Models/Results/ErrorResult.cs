using Lamba.Common.Constants;

namespace Lamba.Common.Models.Results
{
    public sealed record ErrorResult(string Message = ResultMessages.FailedOperation) : Result(false, Message);
    public sealed record ErrorResult<T>(T? Data, string Message = ResultMessages.FailedOperation) : Result<T>(Data, false, Message);
}
