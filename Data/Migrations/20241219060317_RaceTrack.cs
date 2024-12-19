using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaceTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enabled_track1",
                table: "race");

            migrationBuilder.DropColumn(
                name: "end_date_time",
                table: "race");

            migrationBuilder.DropColumn(
                name: "player_track1",
                table: "race");

            migrationBuilder.DropColumn(
                name: "player_track2",
                table: "race");

            migrationBuilder.DropColumn(
                name: "start_date_time",
                table: "race");

            migrationBuilder.RenameColumn(
                name: "enabled_track2",
                table: "race",
                newName: "is_active");

            migrationBuilder.CreateTable(
                name: "race_track",
                columns: table => new
                {
                    race_id = table.Column<int>(type: "integer", nullable: false),
                    track_id = table.Column<int>(type: "integer", nullable: false),
                    player_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_race_track", x => new { x.race_id, x.track_id });
                    table.ForeignKey(
                        name: "fk_race_track_player_player_id",
                        column: x => x.player_id,
                        principalTable: "player",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_race_track_race_race_id",
                        column: x => x.race_id,
                        principalTable: "race",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_race_track_player_id",
                table: "race_track",
                column: "player_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "race_track");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "race",
                newName: "enabled_track2");

            migrationBuilder.AddColumn<bool>(
                name: "enabled_track1",
                table: "race",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date_time",
                table: "race",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "player_track1",
                table: "race",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "player_track2",
                table: "race",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date_time",
                table: "race",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
