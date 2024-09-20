using System.Security.Claims;

namespace Lamba.Security.Abstract
{
    public interface ITokenProvider
    {
        string CreateToken(IEnumerable<Claim>? claims = null);
    }
}
