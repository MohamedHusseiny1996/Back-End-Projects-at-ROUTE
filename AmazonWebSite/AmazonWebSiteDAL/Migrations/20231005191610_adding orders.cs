using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonWebSiteDAL.Migrations
{
    public partial class addingorders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CountOfItems = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
