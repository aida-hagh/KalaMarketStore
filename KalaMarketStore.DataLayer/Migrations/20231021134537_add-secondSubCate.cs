using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KalaMarketStore.DataLayer.Migrations
{
    public partial class addsecondSubCate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecondSubCategoryId",
                table: "Products",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondSubCategoryId",
                table: "Products");
        }
    }
}
