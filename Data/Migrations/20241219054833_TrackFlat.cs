using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class TrackFlat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_player_race_race_id",
                table: "player");

            migrationBuilder.DropIndex(
                name: "ix_player_race_id",
                table: "player");

            migrationBuilder.DropColumn(
                name: "tracks",
                table: "race");

            migrationBuilder.DropColumn(
                name: "race_id",
                table: "player");

            migrationBuilder.AddColumn<bool>(
                name: "track1enabled",
                table: "race",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "track1player",
                table: "race",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "track2enabled",
                table: "race",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "track2player",
                table: "race",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "track1enabled",
                table: "race");

            migrationBuilder.DropColumn(
                name: "track1player",
                table: "race");

            migrationBuilder.DropColumn(
                name: "track2enabled",
                table: "race");

            migrationBuilder.DropColumn(
                name: "track2player",
                table: "race");

            migrationBuilder.AddColumn<List<int>>(
                name: "tracks",
                table: "race",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "race_id",
                table: "player",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_player_race_id",
                table: "player",
                column: "race_id");

            migrationBuilder.AddForeignKey(
                name: "fk_player_race_race_id",
                table: "player",
                column: "race_id",
                principalTable: "race",
                principalColumn: "id");
        }
    }
}
