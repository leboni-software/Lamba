using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lamba.Identity.Infrastructure.Data
{
    public class IdentityDbContextInitializer
    {
        private readonly ILogger<IdentityDbContextInitializer> _logger;
        private readonly IdentityWriterDbContext _writerContext;

        public IdentityDbContextInitializer(ILogger<IdentityDbContextInitializer> logger, IdentityWriterDbContext writerContext)
        {
            _logger = logger;
            _writerContext = writerContext;
        }

        public void Initialize()
        {
            try
            {
                _writerContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }
        public void Seed()
        {
            try
            {
                TrySeed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        public void TrySeed()
        {
            if (!_writerContext.Roles.Any() && !_writerContext.Users.Any() && !_writerContext.Permissions.Any())
            {
                var adminRole = new Role("Admin", true, false);
                _writerContext.Roles.Add(adminRole);
                var permissions = new List<Permission>
                {
                    new() { CommandName = "UpdateUserCommand" },
                    new() { CommandName = "DeleteUserCommand" },
                    new() { CommandName = "GetUserQuery" },
                    new() { CommandName = "CreateRoleCommand" },
                    new() { CommandName = "DeleteRoleCommand" },
                    new() { CommandName = "UpdateRoleCommand" },
                    new() { CommandName = "GetRoleQuery" },
                    new() { CommandName = "GetRolesQuery" },
                    new() { CommandName = "AddUserRoleCommand" },
                    new() { CommandName = "DeleteUserRoleCommand" }
                };
                _writerContext.Permissions.AddRange(permissions);
                foreach (var permission in permissions)
                {
                    _writerContext.PermissionRoles.Add(new PermissionRole
                    {
                        Role = adminRole,
                        Permission = permission
                    });
                }
                var adminUser = new User(
                    "Alperen",
                    "Kucukali",
                    "alperen.kucukali",
                    "alperen.kucukali@hotmail.com",
                    "9^Hc[3q,0l2<At^z@8");
                _writerContext.Users.Add(adminUser);
                _writerContext.UserRoles.Add(new UserRole(adminUser, adminRole));
                _writerContext.Roles.Add(new Role("User", false, true));
                _writerContext.SaveChanges();
            }
        }
    }
    public static class InitializerExtensions
    {
        public static void InitializeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<IdentityDbContextInitializer>();

            initialiser.Initialize();

            initialiser.Seed();
        }
    }
}
