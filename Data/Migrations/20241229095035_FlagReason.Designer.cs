﻿// <auto-generated />
using System;
using LeTrack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeTrack.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241229095035_FlagReason")]
    partial class FlagReason
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LeTrack.Entities.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxFlag")
                        .HasColumnType("integer")
                        .HasColumnName("max_flag");

                    b.Property<int>("MinFlag")
                        .HasColumnType("integer")
                        .HasColumnName("min_flag");

                    b.HasKey("Id")
                        .HasName("pk_config");

                    b.ToTable("config", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer")
                        .HasColumnName("track_id");

                    b.HasKey("Id")
                        .HasName("pk_event");

                    b.ToTable("event", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.Lap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FlagReason")
                        .HasColumnType("text")
                        .HasColumnName("flag_reason");

                    b.Property<bool>("IsFlagged")
                        .HasColumnType("boolean")
                        .HasColumnName("is_flagged");

                    b.Property<TimeSpan?>("LapTime")
                        .HasColumnType("interval")
                        .HasColumnName("lap_time");

                    b.Property<TimeSpan?>("LapTimeDifference")
                        .HasColumnType("interval")
                        .HasColumnName("lap_time_difference");

                    b.Property<Guid?>("LastLapId")
                        .HasColumnType("uuid")
                        .HasColumnName("last_lap_id");

                    b.Property<int>("RaceId")
                        .HasColumnType("integer")
                        .HasColumnName("race_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer")
                        .HasColumnName("track_id");

                    b.HasKey("Id")
                        .HasName("pk_lap");

                    b.ToTable("lap", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NickName")
                        .HasColumnType("text")
                        .HasColumnName("nick_name");

                    b.HasKey("Id")
                        .HasName("pk_player");

                    b.ToTable("player", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time");

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date_time");

                    b.Property<int?>("EndLapCount")
                        .HasColumnType("integer")
                        .HasColumnName("end_lap_count");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("RestartDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("restart_date_time");

                    b.Property<DateTime?>("StartDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date_time");

                    b.Property<TimeSpan?>("TimeRemaining")
                        .HasColumnType("interval")
                        .HasColumnName("time_remaining");

                    b.HasKey("Id")
                        .HasName("pk_race");

                    b.ToTable("race", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.RaceTrack", b =>
                {
                    b.Property<int>("RaceId")
                        .HasColumnType("integer")
                        .HasColumnName("race_id");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer")
                        .HasColumnName("track_id");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer")
                        .HasColumnName("player_id");

                    b.HasKey("RaceId", "TrackId")
                        .HasName("pk_race_track");

                    b.HasIndex("PlayerId")
                        .HasDatabaseName("ix_race_track_player_id");

                    b.ToTable("race_track", (string)null);
                });

            modelBuilder.Entity("LeTrack.Entities.RaceTrack", b =>
                {
                    b.HasOne("LeTrack.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_race_track_player_player_id");

                    b.HasOne("LeTrack.Entities.Race", null)
                        .WithMany("RaceTracks")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_race_track_race_race_id");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("LeTrack.Entities.Race", b =>
                {
                    b.Navigation("RaceTracks");
                });
#pragma warning restore 612, 618
        }
    }
}
