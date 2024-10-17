using Lamba.Identity.Application;
using Lamba.Identity.Infrastructure;
using Lamba.Security;

var builder = WebApplication.CreateBuilder(args);

var isDevelopmentEnv = builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local");
// Add services to the container.
builder.Services.AddLambaIdentityInfrastructureServices(builder.Configuration, isDevelopmentEnv);
builder.Services.AddLambaIdentityApplicationServices(builder.Configuration);
builder.Services.AddLambaSwaggerGenWithAuthServices("v1", $"Lamba {builder.Environment.EnvironmentName} Identity Api");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
