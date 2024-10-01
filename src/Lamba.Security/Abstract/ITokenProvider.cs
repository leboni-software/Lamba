using System.Security.Claims;

namespace Lamba.Security.Abstract
{
    public interface ITokenProvider
    {
        string CreateToken(string username, string role);
    }
}
