using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonWebSiteDAL.Migrations
{
    public partial class makingFKnullableinimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Items_ItemID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ItemID",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ItemID",
                table: "Images",
                column: "ItemID",
                unique: true,
                filter: "[ItemID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Items_ItemID",
                table: "Images",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Items_ItemID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ItemID",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ItemID",
                table: "Images",
                column: "ItemID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Items_ItemID",
                table: "Images",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
