using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class AddedDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "MyRegisteredUsers");

            migrationBuilder.CreateTable(
                name: "RoleVM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleVM",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleVM", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "UserVM",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVM", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleVM");

            migrationBuilder.DropTable(
                name: "UserRoleVM");

            migrationBuilder.DropTable(
                name: "UserVM");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "MyRegisteredUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
