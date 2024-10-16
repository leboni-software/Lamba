using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : BaseAuditEntityConfiguration<Permission, Guid>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.CommandName).HasMaxLength(128).IsRequired();

            builder.HasIndex(x => x.CommandName)
                .IsUnique()
                .HasFilter(NpgsqlEntityConfigurationConstant.DeletedFilter);

            builder.HasMany(x => x.PermissionRoles)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId)
                .IsRequired();
        }
    }
}
