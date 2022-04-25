using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVendas.Migrations
{
    public partial class ForthAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "SaleItem",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "SaleItem");
        }
    }
}
