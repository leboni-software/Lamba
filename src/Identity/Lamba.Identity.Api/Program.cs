using Lamba.Identity.Application;
using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Infrastructure;
using Lamba.Identity.Infrastructure.Data;
using Lamba.Security;
using Lamba.Logger;
using Lamba.Identity.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

var isDevelopmentEnv = builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local");
// Add services to the container.
builder.Services.AddLambaIdentityInfrastructureServices(builder.Configuration, isDevelopmentEnv);
builder.Services.AddLambaIdentityApplicationServices(builder.Configuration);
builder.Services.AddLambaSwaggerGenWithAuthServices("v1", $"Lamba {builder.Environment.EnvironmentName} Identity Api");
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
    opt.Filters.Add<ResultFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Host.UseLambaLogger("lamba-identity");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.InitializeDatabase();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
