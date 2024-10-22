using Lamba.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace Lamba.Identity.Application.Common.Accessors
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? GetId()
        {
            if (Guid.TryParse(GetClaimValue(SecurityConstants.IdentifierClaim), out Guid id))
            {
                return id;
            }
            return null;
        }

        public string? GetRole()
        {
            return GetClaimValue(SecurityConstants.RoleClaim);
        }

        public string? GetUsername()
        {
            return GetClaimValue(SecurityConstants.UsernameClaim);
        }
        private string? GetClaimValue(string claimType)
        {
            return _contextAccessor.HttpContext?.User?.FindFirst(claimType)?.Value;
        }
    }
}
