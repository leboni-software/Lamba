using Lamba.Security.Abstract;
using Lamba.Security.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Lamba.Security.Concrete
{
    public class JwtTokenProvider(IOptions<TokenOptions> options) : ITokenProvider
    {
        private readonly TokenOptions _tokenOptions = options.Value;

        public virtual string CreateToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(LambaSecurityConstants.UsernameClaim, username),
                    new Claim(LambaSecurityConstants.RoleClaim, role)]),
                Expires = DateTime.UtcNow.AddMinutes(_tokenOptions.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience
            };
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
    }
}
