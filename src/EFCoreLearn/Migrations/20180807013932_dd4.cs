using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreLearn.Migrations
{
    public partial class dd4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ddd",
                table: "Order",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ddd",
                table: "Order");
        }
    }
}
