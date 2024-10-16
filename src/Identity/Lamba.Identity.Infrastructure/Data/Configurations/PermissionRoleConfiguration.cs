using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Lamba.Infrastructure.Data.Configurations.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class PermissionRoleConfiguration : BaseAuditEntityConfiguration<PermissionRole, Guid>
    {
        public override void Configure(EntityTypeBuilder<PermissionRole> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => new
            {
                x.PermissionId,
                x.RoleId
            })
            .IsUnique()
            .HasFilter(NpgsqlEntityConfigurationConstant.DeletedFilter);

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.PermissionRoles)
                .HasForeignKey(x => x.PermissionId)
                .IsRequired();
            builder.HasOne(x => x.Role)
                .WithMany(x => x.PermissionRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
