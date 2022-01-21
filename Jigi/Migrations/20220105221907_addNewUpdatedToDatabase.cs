using Microsoft.EntityFrameworkCore.Migrations;

namespace Jigi.Migrations
{
    public partial class addNewUpdatedToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembersRole_Members_SingleMemberRoleId",
                table: "MembersRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryNameId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ShippingDetailsId",
                table: "Shippings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryNameId",
                table: "Products",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryNameId",
                table: "Products",
                newName: "IX_Products_CategoryName");

            migrationBuilder.RenameColumn(
                name: "SingleMemberRoleId",
                table: "MembersRole",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "MemberRoleId",
                table: "MembersRole",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_MembersRole_SingleMemberRoleId",
                table: "MembersRole",
                newName: "IX_MembersRole_MemberId");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Members",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MembersRole_Members_MemberId",
                table: "MembersRole",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryName",
                table: "Products",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembersRole_Members_MemberId",
                table: "MembersRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryName",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shippings",
                newName: "ShippingDetailsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Products",
                newName: "CategoryNameId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryName",
                table: "Products",
                newName: "IX_Products_CategoryNameId");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "MembersRole",
                newName: "SingleMemberRoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MembersRole",
                newName: "MemberRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MembersRole_MemberId",
                table: "MembersRole",
                newName: "IX_MembersRole_SingleMemberRoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembersRole_Members_SingleMemberRoleId",
                table: "MembersRole",
                column: "SingleMemberRoleId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryNameId",
                table: "Products",
                column: "CategoryNameId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
