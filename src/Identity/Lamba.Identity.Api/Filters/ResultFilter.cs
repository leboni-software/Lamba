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
                if (objectResult.Value is null)
                    executedContext.Result = new OkObjectResult(new SuccessResult("Operation completed successfully!"));
                else
                    executedContext.Result = new OkObjectResult(new SuccessResult<object>(objectResult.Value, "Operation completed successfully!"));
            }
        }
    }
}
