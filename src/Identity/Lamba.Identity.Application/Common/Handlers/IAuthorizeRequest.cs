using Lamba.Identity.Application.Common.Accessors;
using MediatR;

namespace Lamba.Identity.Application.Common.Handlers
{
    public interface IAuthorizeRequest<TResponse> : IRequest<TResponse>
    {
    }
    public interface IAuthorizeRequest : IRequest
    {
    }
}
