using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lamba.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class role_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsMasterRole",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultRole",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDefaultRole",
                table: "Roles",
                column: "IsDefaultRole",
                unique: true,
                filter: "\"IsDefaultRole\" = true AND \"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsMasterRole",
                table: "Roles",
                column: "IsMasterRole",
                unique: true,
                filter: "\"IsMasterRole\" = true AND \"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Roles_IsDefaultRole",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_IsMasterRole",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsDefaultRole",
                table: "Roles");

            migrationBuilder.AlterColumn<bool>(
                name: "IsMasterRole",
                table: "Roles",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);
        }
    }
}
