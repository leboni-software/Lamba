using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Security.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                _logger.LogError(ex, "An error occurred while initialising the database.");
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
            if (!_writerContext.Roles.Any() && !_writerContext.Users.Any())
            {
                var adminRole = new Role
                {
                    Name = "Admin",
                    IsDefaultRole = false,
                    IsMasterRole = true
                };
                _writerContext.Roles.Add(adminRole);
                var salt = HashHelper.GenerateSalt();
                var adminUser = new User
                {
                    FirstName = "Alperen",
                    LastName = "Kucukali",
                    Email = "alperen.kucukali@hotmail.com",
                    Username = "alperen.kucukali",
                    Password = HashHelper.ComputeHash("9^Hc[3q,0l2<At^z@8", salt),
                    PasswordSalt = salt
                };
                _writerContext.Users.Add(adminUser);
                _writerContext.UserRoles.Add(new UserRole
                {
                    Role = adminRole,
                    User = adminUser
                });
                _writerContext.Roles.Add(new Role
                {
                    Name = "User",
                    IsDefaultRole = true,
                    IsMasterRole = false
                });
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
