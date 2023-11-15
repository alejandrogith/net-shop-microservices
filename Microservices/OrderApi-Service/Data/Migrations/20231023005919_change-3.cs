using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderApi_Service.Data.Migrations
{
    public partial class change3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrderMaster",
                newName: "EmailUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailUser",
                table: "OrderMaster",
                newName: "UserId");
        }
    }
}
