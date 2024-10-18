using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Common.Models.Results
{
    public record Result(bool IsSuccess, string Message);
    public record Result<T>(T? Data, bool IsSuccess, string Message) : Result(IsSuccess, Message);
}
