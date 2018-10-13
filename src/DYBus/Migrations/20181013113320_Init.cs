using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DYBus.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusRunTimeInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoadNum = table.Column<string>(nullable: true),
                    BusCarNo = table.Column<string>(nullable: true),
                    BusRunDirection = table.Column<string>(nullable: true),
                    BusStatus = table.Column<string>(nullable: true),
                    BeforeStationNo = table.Column<string>(nullable: true),
                    CurrentStationNo = table.Column<string>(nullable: true),
                    IsBusStop = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRunTimeInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusRunTimeInfos");
        }
    }
}
