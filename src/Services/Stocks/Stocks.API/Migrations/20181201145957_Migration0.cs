using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.API.Migrations
{
    public partial class Migration0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Isin = table.Column<string>(maxLength: 12, nullable: false),
                    Name = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Isin);
                });

            migrationBuilder.CreateTable(
                name: "StockQuotations",
                columns: table => new
                {
                    Isin = table.Column<string>(maxLength: 12, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Open = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Volume = table.Column<long>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Transactions = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockQuotations", x => new { x.Isin, x.Date });
                    table.ForeignKey(
                        name: "FK_StockQuotations_Stocks_Isin",
                        column: x => x.Isin,
                        principalTable: "Stocks",
                        principalColumn: "Isin",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockQuotations");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
