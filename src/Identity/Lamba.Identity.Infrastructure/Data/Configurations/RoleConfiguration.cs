using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : BaseAuditEntityConfiguration<Role, Guid>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.IsMasterRole).HasDefaultValue(false).IsRequired();
            builder.Property(x => x.IsDefaultRole).HasDefaultValue(false).IsRequired();

            builder.HasIndex(x => x.IsMasterRole)
                .IsUnique()
                .HasFilter($"\"{nameof(Role.IsMasterRole)}\" = true AND {NpgsqlEntityConfigurationConstant.DeletedFilter}");
            builder.HasIndex(x => x.IsDefaultRole)
                .IsUnique()
                .HasFilter($"\"{nameof(Role.IsDefaultRole)}\" = true AND {NpgsqlEntityConfigurationConstant.DeletedFilter}");
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.ToTable(x => x.HasCheckConstraint("CK_Role_IsMasterRole_IsDefaultRole", $"\"{nameof(Role.IsMasterRole)}\" = false OR \"{nameof(Role.IsDefaultRole)}\" = false"));

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .IsRequired();

            builder.HasMany(x => x.PermissionRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
