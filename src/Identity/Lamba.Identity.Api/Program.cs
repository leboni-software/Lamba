using Lamba.Identity.Api.Filters;
using Lamba.Identity.Application;
using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Infrastructure;
using Lamba.Identity.Infrastructure.Data;
using Lamba.Logger;
using Lamba.Security.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var isDevelopmentEnv = builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local");
// Add services to the container.
builder.Services.AddLambaIdentityInfrastructureServices(builder.Configuration, isDevelopmentEnv);
builder.Services.AddLambaIdentityApplicationServices(builder.Configuration);
//builder.Services.AddLambaSwaggerGenWithAuthServices("v1", $"Lamba {builder.Environment.EnvironmentName} Identity Api");
builder.Services.AddOpenApi(opt => { opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
    opt.Filters.Add<ResultFilter>();
});
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Host.UseLambaLogger("lamba-identity");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle($"Lamba {builder.Environment.EnvironmentName} Identity Api")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme)
        .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme, auth =>
        {
            auth.Description = "Enter your token";
        });
    });
    app.UseDeveloperExceptionPage();
    app.InitializeDatabase();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
