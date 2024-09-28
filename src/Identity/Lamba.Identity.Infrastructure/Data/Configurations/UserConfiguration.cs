using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class UserConfiguration : BaseAuditEntityConfiguration<User, Guid>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.FirstName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(256).IsRequired();
            builder.Property(x => x.PasswordSalt).HasMaxLength(256).IsRequired();

            builder.HasIndex(x => x.Username)
                .IsUnique()
                .HasFilter(NpgsqlEntityConfigurationConstant.DeletedFilter);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .IsRequired();
        }
    }
}
