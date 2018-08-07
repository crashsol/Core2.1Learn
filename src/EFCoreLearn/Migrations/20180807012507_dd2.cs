using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreLearn.Migrations
{
    public partial class dd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    Url = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    Test1 = table.Column<string>(maxLength: 80, nullable: true),
                    Test2 = table.Column<string>(maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 80, nullable: true),
                    Content = table.Column<string>(maxLength: 80, nullable: true),
                    BlogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Street = table.Column<string>(maxLength: 80, nullable: true),
                    City = table.Column<string>(maxLength: 80, nullable: true),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Address_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "BlogId", "Name", "Url" },
                values: new object[] { 1, "1", "www" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 1, 1, "123", "123" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 2, 1, "123", "123" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "BlogId", "Content", "Title" },
                values: new object[] { 3, 1, "123", "123" });

            migrationBuilder.CreateIndex(
                name: "IX_Post_BlogId",
                table: "Post",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
