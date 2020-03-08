using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Askianoor.Migrations
{
    public partial class AddBirthdayDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthdayDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthdayDate",
                table: "AspNetUsers");
        }
    }
}
