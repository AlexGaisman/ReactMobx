using Microsoft.EntityFrameworkCore.Migrations;

namespace Tracker.Migrations
{
    public partial class InventorynewcolumnItemNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemNumber",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemNumber",
                table: "Inventories");
        }
    }
}
