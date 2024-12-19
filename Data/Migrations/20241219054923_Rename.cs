using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "track2player",
                table: "race",
                newName: "player_track2");

            migrationBuilder.RenameColumn(
                name: "track2enabled",
                table: "race",
                newName: "enabled_track2");

            migrationBuilder.RenameColumn(
                name: "track1player",
                table: "race",
                newName: "player_track1");

            migrationBuilder.RenameColumn(
                name: "track1enabled",
                table: "race",
                newName: "enabled_track1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "player_track2",
                table: "race",
                newName: "track2player");

            migrationBuilder.RenameColumn(
                name: "player_track1",
                table: "race",
                newName: "track1player");

            migrationBuilder.RenameColumn(
                name: "enabled_track2",
                table: "race",
                newName: "track2enabled");

            migrationBuilder.RenameColumn(
                name: "enabled_track1",
                table: "race",
                newName: "track1enabled");
        }
    }
}
