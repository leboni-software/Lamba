using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Common.Models.Results
{
    public sealed record ErrorResult(string Message) : Result(false, Message);
    public sealed record ErrorResult<T>(T? Data, string Message) : Result<T>(Data, false, Message);
}
