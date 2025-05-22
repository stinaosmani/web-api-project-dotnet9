using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Oracle_Casing_For_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "USERS");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "POSTS");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "USERS",
                newName: "USERNAME");

            migrationBuilder.RenameColumn(
                name: "SettingsJson",
                table: "USERS",
                newName: "SETTINGSJSON");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "USERS",
                newName: "PASSWORD");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "USERS",
                newName: "LASTNAME");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "USERS",
                newName: "FIRSTNAME");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "USERS",
                newName: "DATEOFBIRTH");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "USERS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "POSTS",
                newName: "TITLE");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "POSTS",
                newName: "SLUG");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "POSTS",
                newName: "DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "POSTS",
                newName: "BODY");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "POSTS",
                newName: "AUTHORID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "POSTS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "POSTS",
                newName: "IX_POSTS_AUTHORID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS",
                table: "USERS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_POSTS",
                table: "POSTS",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_POSTS_USERS_AUTHORID",
                table: "POSTS",
                column: "AUTHORID",
                principalTable: "USERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_POSTS_USERS_AUTHORID",
                table: "POSTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_POSTS",
                table: "POSTS");

            migrationBuilder.RenameTable(
                name: "USERS",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "POSTS",
                newName: "Posts");

            migrationBuilder.RenameColumn(
                name: "USERNAME",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "SETTINGSJSON",
                table: "Users",
                newName: "SettingsJson");

            migrationBuilder.RenameColumn(
                name: "PASSWORD",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "LASTNAME",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FIRSTNAME",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "DATEOFBIRTH",
                table: "Users",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TITLE",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "SLUG",
                table: "Posts",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "Posts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "BODY",
                table: "Posts",
                newName: "Body");

            migrationBuilder.RenameColumn(
                name: "AUTHORID",
                table: "Posts",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_POSTS_AUTHORID",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
