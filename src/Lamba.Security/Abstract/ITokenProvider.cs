namespace Lamba.Security.Abstract
{
    public interface ITokenProvider
    {
        string CreateToken(Guid id, string username, string role);
    }
}
