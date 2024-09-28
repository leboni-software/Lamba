using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lamba.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_updated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Role_IsMasterRole_IsDefaultRole",
                table: "Roles",
                sql: "\"IsMasterRole\" = false OR \"IsDefaultRole\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Role_IsMasterRole_IsDefaultRole",
                table: "Roles");
        }
    }
}
