using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi_Service.Data.Migrations
{
    public partial class change5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Inventory");

            migrationBuilder.AddColumn<string>(
                name: "ProductSKU",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSKU",
                table: "Inventory");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
