using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt.Api.Migrations
{
    /// <inheritdoc />
    public partial class upravaukonu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukonys_VybaveniUkonys_VybaveniUkonyId",
                table: "Ukonys");

            migrationBuilder.DropTable(
                name: "VybaveniUkonys");

            migrationBuilder.RenameColumn(
                name: "VybaveniUkonyId",
                table: "Ukonys",
                newName: "VybaveniId");

            migrationBuilder.RenameIndex(
                name: "IX_Ukonys_VybaveniUkonyId",
                table: "Ukonys",
                newName: "IX_Ukonys_VybaveniId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ukonys_Vybavenis_VybaveniId",
                table: "Ukonys",
                column: "VybaveniId",
                principalTable: "Vybavenis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ukonys_Vybavenis_VybaveniId",
                table: "Ukonys");

            migrationBuilder.RenameColumn(
                name: "VybaveniId",
                table: "Ukonys",
                newName: "VybaveniUkonyId");

            migrationBuilder.RenameIndex(
                name: "IX_Ukonys_VybaveniId",
                table: "Ukonys",
                newName: "IX_Ukonys_VybaveniUkonyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ukonys_VybaveniUkonys_VybaveniUkonyId",
                table: "Ukonys",
                column: "VybaveniUkonyId",
                principalTable: "VybaveniUkonys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
