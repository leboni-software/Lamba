using Lamba.Identity.Application.Common.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Common.Handlers
{
    public abstract class BaseAuthorizeRequestHandler<TRequest, TResponse> : IAuthorizeRequestHandler<TRequest, TResponse>
        where TRequest : BaseAuthorizeRequest<TResponse>
    {
        protected readonly ICurrentUserAccessor? _currentUserAccessor;

        protected BaseAuthorizeRequestHandler(ICurrentUserAccessor? currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
        }

        protected BaseAuthorizeRequestHandler() { }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
    public abstract class BaseAuthorizeRequestHandler<TRequest> : IAuthorizeRequestHandler<TRequest>
        where TRequest : BaseAuthorizeRequest
    {
        protected readonly ICurrentUserAccessor? _currentUserAccessor;

        protected BaseAuthorizeRequestHandler(ICurrentUserAccessor? currentUserAccessor)
        {
            _currentUserAccessor = currentUserAccessor;
        }

        protected BaseAuthorizeRequestHandler() { }

        public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
