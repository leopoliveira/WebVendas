using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVendas.Migrations
{
    public partial class AddSaleNumberSaleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleNumber",
                table: "Sale",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleNumber",
                table: "Sale");
        }
    }
}
