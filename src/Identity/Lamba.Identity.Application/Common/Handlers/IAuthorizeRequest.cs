using MediatR;

namespace Lamba.Identity.Application.Common.Handlers
{
    public interface IAuthorizeRequest<TResponse> : IRequest<TResponse>
    {
        List<string> Roles { get; set; }
    }
    public interface IAuthorizeRequest : IRequest
    {
        List<string> Roles { get; set; }
    }
}
