﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HotelApi.Migrations
{
    public partial class AddHotelHotelRoomBedTypeRoomTypeRoomReservationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BedTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedTypes", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(type: "int4", nullable: false),
                    HotelId = table.Column<long>(type: "int8", nullable: false),
                    BedTypeId = table.Column<int>(type: "int4", nullable: false),
                    BedTypeId1 = table.Column<int>(type: "int4", nullable: true),
                    BedTypeName = table.Column<string>(type: "text", nullable: true),
                    NightlyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    NumberOfBeds = table.Column<int>(type: "int4", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int4", nullable: false),
                    RoomTypeId1 = table.Column<int>(type: "int4", nullable: true),
                    RoomTypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => new { x.RoomNumber, x.HotelId });
                    table.UniqueConstraint("AK_HotelRooms_HotelId_RoomNumber", x => new { x.HotelId, x.RoomNumber });
                    table.ForeignKey(
                        name: "FK_HotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_BedTypes_BedTypeId1_BedTypeName",
                        columns: x => new { x.BedTypeId1, x.BedTypeName },
                        principalTable: "BedTypes",
                        principalColumns: new[] { "Id", "Name" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HotelRooms_RoomTypes_RoomTypeId1_RoomTypeName",
                        columns: x => new { x.RoomTypeId1, x.RoomTypeName },
                        principalTable: "RoomTypes",
                        principalColumns: new[] { "Id", "Name" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomReservations",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int4", nullable: false),
                    HotelId = table.Column<long>(type: "int8", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomReservations", x => new { x.RoomId, x.HotelId, x.StartDate, x.EndDate });
                    table.UniqueConstraint("AK_RoomReservations_EndDate_HotelId_RoomId_StartDate", x => new { x.EndDate, x.HotelId, x.RoomId, x.StartDate });
                    table.ForeignKey(
                        name: "FK_RoomReservations_HotelRooms_RoomId_HotelId",
                        columns: x => new { x.RoomId, x.HotelId },
                        principalTable: "HotelRooms",
                        principalColumns: new[] { "RoomNumber", "HotelId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_BedTypeId1_BedTypeName",
                table: "HotelRooms",
                columns: new[] { "BedTypeId1", "BedTypeName" });

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_RoomTypeId1_RoomTypeName",
                table: "HotelRooms",
                columns: new[] { "RoomTypeId1", "RoomTypeName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomReservations");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "BedTypes");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
