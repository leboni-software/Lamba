using MediatR;
using System.Security.Claims;

namespace Lamba.Identity.Application.Common.Handlers
{
    public interface IAuthorizeRequest<TResponse> : IRequest<TResponse>
    {
    }
    public interface IAuthorizeRequest : IRequest
    {
    }
}
