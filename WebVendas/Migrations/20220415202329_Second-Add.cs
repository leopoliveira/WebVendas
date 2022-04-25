using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVendas.Migrations
{
    public partial class SecondAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItem_Sale_SaleId",
                table: "SaleItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "SaleItem",
                newName: "SaleIdForeingKeySaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem",
                newName: "IX_SaleItem_SaleIdForeingKeySaleId");

            migrationBuilder.AddColumn<double>(
                name: "TotalValue",
                table: "SaleItem",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Sale",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "curdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItem_Sale_SaleIdForeingKeySaleId",
                table: "SaleItem",
                column: "SaleIdForeingKeySaleId",
                principalTable: "Sale",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
