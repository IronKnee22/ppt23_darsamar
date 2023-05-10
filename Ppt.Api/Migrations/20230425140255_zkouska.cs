using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt.Api.Migrations
{
    /// <inheritdoc />
    public partial class zkouska : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybaveni",
                table: "Vybaveni");

            migrationBuilder.RenameTable(
                name: "Vybaveni",
                newName: "Vybavenis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis");

            migrationBuilder.RenameTable(
                name: "Vybavenis",
                newName: "Vybaveni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybaveni",
                table: "Vybaveni",
                column: "Id");
        }
    }
}
