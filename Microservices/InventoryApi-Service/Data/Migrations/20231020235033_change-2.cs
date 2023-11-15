using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi_Service.Data.Migrations
{
    public partial class change2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Inventory",
                newName: "Stock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Inventory",
                newName: "Count");
        }
    }
}
