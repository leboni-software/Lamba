using Lamba.Common.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lamba.Identity.Api.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ErrorResult(context.Exception.Message));
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
