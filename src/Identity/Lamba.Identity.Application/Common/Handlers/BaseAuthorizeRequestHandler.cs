using Lamba.Identity.Application.Common.Accessors;

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
