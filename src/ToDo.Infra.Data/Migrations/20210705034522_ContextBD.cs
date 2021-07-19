using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Infra.Data.Migrations
{
    public partial class ContextBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(150)", nullable: true),
                    Author = table.Column<string>(type: "varchar(150)", nullable: true),
                    Synopsis = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Url = table.Column<string>(type: "varchar(500)", nullable: true),
                    Available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
