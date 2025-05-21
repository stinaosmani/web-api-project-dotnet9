using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Added_Auditable_Fields_On_User_And_Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Users",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Users",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Users",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Users",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Posts",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Posts",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Posts",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Posts",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Posts",
                type: "TIMESTAMP(7)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Posts",
                type: "NVARCHAR2(2000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Posts");
        }
    }
}
