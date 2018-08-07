using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreLearn.Migrations
{
    public partial class dd6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestGuid",
                table: "Order",
                nullable: false,
                defaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestGuid",
                table: "Order");
        }
    }
}
