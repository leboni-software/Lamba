using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Security.Abstract;
using Lamba.Security.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lamba.Identity.Application.Common.Behaviours
{
    public class AuthorizePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseAuthorizeRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRoleReaderRepository _roleReaderRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizePipelineBehavior(ILogger<TRequest> logger, ITokenProvider tokenProvider, IRoleReaderRepository roleReaderRepository, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _roleReaderRepository = roleReaderRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            try
            {
                var roles = _contextAccessor.HttpContext!.User.Claims
                    .Where(x => x.Type == LambaSecurityConstants.RoleClaim)
                    .Select(x => x.Value.Split(','))
                    .FirstOrDefault() ?? throw new Exception("The user does not have any role!");
                var hasPermission = await _roleReaderRepository.GetQueryable()
                    .Where(x => roles.Contains(x.Name))
                    .Where(x => x.PermissionRoles.Any(perm => perm.Permission.CommandName == requestName))
                    .AnyAsync(cancellationToken);
                if (!hasPermission) throw new UnauthorizedAccessException();
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unauthorized access detected! {requestName} {request}");
                throw;
            }
        }
    }
}
