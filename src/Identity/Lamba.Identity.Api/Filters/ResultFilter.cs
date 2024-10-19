using Lamba.Common.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lamba.Identity.Api.Filters
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
