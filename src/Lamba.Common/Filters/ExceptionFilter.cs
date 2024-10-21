using Lamba.Common.Models.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Lamba.Common.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                InvalidOperationException => HttpStatusCode.Forbidden,
                Exception => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            var resultValue = (context.Result as ObjectResult)?.Value;
            context.Result = new ObjectResult(
                resultValue is null
                    ? new ErrorResult(context.Exception.Message)
                    : new ErrorResult<object>(resultValue, context.Exception.Message)
            )
            {
                StatusCode = (int)statusCode
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
