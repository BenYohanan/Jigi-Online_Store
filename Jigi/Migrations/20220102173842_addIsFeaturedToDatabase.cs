using Microsoft.EntityFrameworkCore.Migrations;

namespace Jigi.Migrations
{
    public partial class addIsFeaturedToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImage",
                table: "Products",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "PoductTd",
                table: "Products",
                newName: "PoductId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "ProductImage");

            migrationBuilder.RenameColumn(
                name: "PoductId",
                table: "Products",
                newName: "PoductTd");
        }
    }
}
