using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lamba.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "UserRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "UserRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "UserRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "Permissions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserId",
                table: "PermissionRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "PermissionRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserId",
                table: "PermissionRoles",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "PermissionRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "PermissionRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "PermissionRoles");
        }
    }
}
