using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class newMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccounts_BankAccounts_AccountNum",
                table: "ClientAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccounts_Clients_ClientID",
                table: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "MyRegisteredUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductOrderVM");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccounts_BankAccounts_AccountNum",
                table: "ClientAccounts",
                column: "AccountNum",
                principalTable: "BankAccounts",
                principalColumn: "AccountNum",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccounts_Clients_ClientID",
                table: "ClientAccounts",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccounts_BankAccounts_AccountNum",
                table: "ClientAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAccounts_Clients_ClientID",
                table: "ClientAccounts");

            migrationBuilder.CreateTable(
                name: "MyRegisteredUsers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRegisteredUsers", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderVM",
                columns: table => new
                {
                    ProductOrderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discounted = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderVM", x => x.ProductOrderID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false),
                    Discounted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => new { x.ProductID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, 23.48m, "Oranges" },
                    { 2, 38.45m, "Apples" },
                    { 3, 38.45m, "Royce's Peaches" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "ProductID", "Discounted", "Quantity" },
                values: new object[,]
                {
                    { 1000, 1, false, 10 },
                    { 1001, 2, true, 23 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccounts_BankAccounts_AccountNum",
                table: "ClientAccounts",
                column: "AccountNum",
                principalTable: "BankAccounts",
                principalColumn: "AccountNum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAccounts_Clients_ClientID",
                table: "ClientAccounts",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
