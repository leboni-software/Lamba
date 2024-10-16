namespace Lamba.Identity.Application.Common.Handlers
{
    public class BaseAuthorizeRequest<TResponse> : IAuthorizeRequest<TResponse>
    {
        public required List<string> Roles { get; set; }
    }
}
