using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Askianoor.Migrations
{
    public partial class AddSkillsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    cssClass = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder != null)
            {
                migrationBuilder.DropTable(
                name: "Skills");
            }
        }
    }
}
