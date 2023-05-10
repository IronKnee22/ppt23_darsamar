using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt.Api.Migrations
{
    /// <inheritdoc />
    public partial class Pridani_zbytku : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BuyDate",
                table: "Vybaveni",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Cena",
                table: "Vybaveni",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRevision",
                table: "Vybaveni",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyDate",
                table: "Vybaveni");

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "Vybaveni");

            migrationBuilder.DropColumn(
                name: "LastRevision",
                table: "Vybaveni");
        }
    }
}
