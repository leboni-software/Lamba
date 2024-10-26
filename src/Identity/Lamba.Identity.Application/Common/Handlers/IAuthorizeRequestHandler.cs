using MediatR;

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
