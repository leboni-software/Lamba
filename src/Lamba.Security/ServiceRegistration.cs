using Lamba.Security.Abstract;
using Lamba.Security.Common;
using Lamba.Security.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lamba.Security
{
    public static class ServiceRegistration
    {
        public static void AddLambaSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<TokenOptions>().Bind(configuration.GetSection(nameof(TokenOptions)));
            services.AddSingleton<ITokenProvider, JwtTokenProvider>();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[$"{nameof(TokenOptions)}:{nameof(TokenOptions.SecretKey)}"]!)),
                        ValidIssuer = configuration[$"{nameof(TokenOptions)}:{nameof(TokenOptions.Issuer)}"]!,
                        ValidAudience = configuration[$"{nameof(TokenOptions)}:{nameof(TokenOptions.Audience)}"]!,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
