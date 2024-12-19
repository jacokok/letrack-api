using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeTrack.Data.Migrations
{
    /// <inheritdoc />
    public partial class RaceDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "end_date_time",
                table: "race",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end_lap_count",
                table: "race",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date_time",
                table: "race",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date_time",
                table: "race");

            migrationBuilder.DropColumn(
                name: "end_lap_count",
                table: "race");

            migrationBuilder.DropColumn(
                name: "start_date_time",
                table: "race");
        }
    }
}
