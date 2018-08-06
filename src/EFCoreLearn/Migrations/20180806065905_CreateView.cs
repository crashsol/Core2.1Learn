using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreLearn.Migrations
{
    public partial class CreateView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view View_BlogPostCounts as 

select a.Name,Count(a.Name) as PostCount from Blog as a left join Post as b on a.BlogId  = b.BlogId
group by a.Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
