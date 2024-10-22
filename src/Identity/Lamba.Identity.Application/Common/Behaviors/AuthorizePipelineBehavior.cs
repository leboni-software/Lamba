using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Security.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lamba.Identity.Application.Common.Behaviors
{
    public class AuthorizePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseAuthorizeRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRoleReaderRepository _roleReaderRepository;
        private readonly ICurrentUserAccessor _currentUser;

        public AuthorizePipelineBehavior(ILogger<TRequest> logger, ITokenProvider tokenProvider, IRoleReaderRepository roleReaderRepository, ICurrentUserAccessor currentUser)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _roleReaderRepository = roleReaderRepository;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            try
            {
                var roles = _currentUser.GetRole() ?? throw new Exception("The user does not have any role!");
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
