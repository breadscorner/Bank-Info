using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class seedBankData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "AccountNum", "AccountType", "Balance" },
                values: new object[,]
                {
                    { 1, "Savings", 1000.0m },
                    { 2, "Checking", 2500.0m }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientID", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "ClientAccounts",
                columns: new[] { "AccountNum", "ClientID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientAccounts",
                keyColumns: new[] { "AccountNum", "ClientID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ClientAccounts",
                keyColumns: new[] { "AccountNum", "ClientID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNum",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNum",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 2);
        }
    }
}
