using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt.Api.Migrations
{
    /// <inheritdoc />
    public partial class Ukony : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VybaveniUkonys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VybaveniUkonys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ukonys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    VybaveniUkonyId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ukonys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ukonys_VybaveniUkonys_VybaveniUkonyId",
                        column: x => x.VybaveniUkonyId,
                        principalTable: "VybaveniUkonys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ukonys_VybaveniUkonyId",
                table: "Ukonys",
                column: "VybaveniUkonyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ukonys");

            migrationBuilder.DropTable(
                name: "VybaveniUkonys");
        }
    }
}
