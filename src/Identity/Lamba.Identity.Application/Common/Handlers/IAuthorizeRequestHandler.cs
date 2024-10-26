using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Common.Handlers
{
    public interface IAuthorizeRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : BaseAuthorizeRequest<TResponse>
    {
    }
    public interface IAuthorizeRequestHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : BaseAuthorizeRequest
    {
    }
}
