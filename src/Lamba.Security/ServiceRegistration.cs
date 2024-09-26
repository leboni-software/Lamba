using Lamba.Security.Abstract;
using Lamba.Security.Common;
using Lamba.Security.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        public static void AddLambaSwaggerGenWithAuthServices(this IServiceCollection services, string version = "v1", string title = "Lamba Api")
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version
                });
                setup.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter your token",
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Type = SecuritySchemeType.Http
                });
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        []
                    }
                });
            });
        }
    }
}
