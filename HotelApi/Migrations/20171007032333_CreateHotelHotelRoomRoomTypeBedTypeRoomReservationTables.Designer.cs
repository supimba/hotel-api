﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PostgresEFCore.Providers;
using System;

namespace HotelApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20171007032333_CreateHotelHotelRoomRoomTypeBedTypeRoomReservationTables")]
    partial class CreateHotelHotelRoomRoomTypeBedTypeRoomReservationTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Common.Models.BedType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id", "Name");

                    b.ToTable("BedTypes");
                });

            modelBuilder.Entity("Common.Models.Hotel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Common.Models.HotelRoom", b =>
                {
                    b.Property<long>("RoomNumber");

                    b.Property<long>("HotelId");

                    b.Property<int>("BedTypeId");

                    b.Property<int?>("BedTypeId1");

                    b.Property<string>("BedTypeName");

                    b.Property<decimal>("NightlyRate");

                    b.Property<int>("NumberOfBeds");

                    b.Property<int>("RoomTypeId");

                    b.Property<int?>("RoomTypeId1");

                    b.Property<string>("RoomTypeName");

                    b.HasKey("RoomNumber", "HotelId");

                    b.HasAlternateKey("HotelId", "RoomNumber");

                    b.HasIndex("BedTypeId1", "BedTypeName");

                    b.HasIndex("RoomTypeId1", "RoomTypeName");

                    b.ToTable("HotelRooms");
                });

            modelBuilder.Entity("Common.Models.RoomReservation", b =>
                {
                    b.Property<long>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<long>("HotelId");

                    b.Property<long>("RoomNumber");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("ReservationId");

                    b.HasIndex("RoomNumber", "HotelId");

                    b.ToTable("RoomReservations");
                });

            modelBuilder.Entity("Common.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id", "Name");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("Common.Models.HotelRoom", b =>
                {
                    b.HasOne("Common.Models.Hotel", "Hotel")
                        .WithMany("HotelRooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Models.BedType", "BedType")
                        .WithMany()
                        .HasForeignKey("BedTypeId1", "BedTypeName");

                    b.HasOne("Common.Models.RoomType", "RoomType")
                        .WithMany()
                        .HasForeignKey("RoomTypeId1", "RoomTypeName");
                });

            modelBuilder.Entity("Common.Models.RoomReservation", b =>
                {
                    b.HasOne("Common.Models.HotelRoom", "HotelRoom")
                        .WithMany("RoomReservations")
                        .HasForeignKey("RoomNumber", "HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}