using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Security.Abstract;
using Lamba.Security.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Common.Behaviours
{
    public class AuthorizePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRoleReaderRepository _roleReaderRepository;

        public AuthorizePipelineBehaviour(ILogger<TRequest> logger, ITokenProvider tokenProvider, IRoleReaderRepository roleReaderRepository)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
            _roleReaderRepository = roleReaderRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (request is IAuthorizeRequest<TResponse> authRequest)
                {
                    var requestName = typeof(TRequest).Name;
                    var hasPermission = await _roleReaderRepository.GetQueryable()
                        .Where(x => authRequest.Roles.Contains(x.Name))
                        .Where(x => x.PermissionRoles.Any(perm => perm.Permission.CommandName == requestName))
                        .AnyAsync(cancellationToken);
                    if (!hasPermission) throw new UnauthorizedAccessException();
                }
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Authorize Error Occured! {typeof(TRequest)} {request}");
                throw;
            }
        }
    }
}
