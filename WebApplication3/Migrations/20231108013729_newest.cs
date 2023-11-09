using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class newest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientAccountVM",
                columns: table => new
                {
                    AccountNum = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientID = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountType = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    ClientAccountVMAccountNum = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccountVM", x => x.AccountNum);
                    table.ForeignKey(
                        name: "FK_ClientAccountVM_ClientAccountVM_ClientAccountVMAccountNum",
                        column: x => x.ClientAccountVMAccountNum,
                        principalTable: "ClientAccountVM",
                        principalColumn: "AccountNum");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccountVM_ClientAccountVMAccountNum",
                table: "ClientAccountVM",
                column: "ClientAccountVMAccountNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAccountVM");
        }
    }
}
