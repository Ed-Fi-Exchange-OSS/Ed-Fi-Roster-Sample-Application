using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EdFi.Roster.Data.Migrations
{
    public partial class CreateBearerTokenstableandApiSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Secret = table.Column<string>(type: "TEXT", nullable: true),
                    RootUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BearerTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    DateTimeCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiresIn = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BearerTokens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiSettings");

            migrationBuilder.DropTable(
                name: "BearerTokens");
        }
    }
}
