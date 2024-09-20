namespace Lamba.Security.Common
{
    public class TokenOptions
    {
        public virtual string SecretKey { get; set; } = null!;
        public virtual string Audience { get; set; } = null!;
        public virtual string Issuer { get; set; } = null!;
        public virtual int ExpirationInMinutes { get; set; }
    }
}
