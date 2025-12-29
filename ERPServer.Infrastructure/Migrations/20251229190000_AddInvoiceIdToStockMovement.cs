using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceIdToStockMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "StockMovements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_InvoiceId",
                table: "StockMovements",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Invoices_InvoiceId",
                table: "StockMovements",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Invoices_InvoiceId",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_InvoiceId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "StockMovements");
        }
    }
}
