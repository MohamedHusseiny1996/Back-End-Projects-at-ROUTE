using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Department_project_DAL.Migrations
{
    public partial class addingdateofcreationforuserapplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "AspNetUsers");
        }
    }
}
