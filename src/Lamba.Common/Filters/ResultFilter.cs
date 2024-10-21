using Lamba.Common.Models.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Common.Filters
{
    public class ResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();
            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is not Result)
            {
                executedContext.Result = new OkObjectResult(
                    objectResult.Value is null
                    ? new SuccessResult()
                    : new SuccessResult<object>(objectResult.Value)
                );
            }
        }
    }
}
