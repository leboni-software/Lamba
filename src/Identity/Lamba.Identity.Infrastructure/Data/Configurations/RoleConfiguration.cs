using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lamba.Identity.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : BaseAuditEntityConfiguration<Role, Guid>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
            builder.Property(x => x.IsMasterRole).IsRequired();

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .IsRequired();
        }
    }
}
