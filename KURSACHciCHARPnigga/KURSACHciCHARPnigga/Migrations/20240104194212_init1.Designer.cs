﻿// <auto-generated />
using System;
using KURSACHciCHARPnigga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KURSACHciCHARPnigga.Migrations
{
    [DbContext(typeof(MyDB))]
    [Migration("20240104194212_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KURSACHciCHARPnigga.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FirstRoomId")
                        .HasColumnType("int");

                    b.Property<string>("NameOfRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SecondRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("ThirdRoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstRoomId = 2,
                            NameOfRoom = "Room 1",
                            SecondRoomId = 3,
                            ThirdRoomId = 4
                        },
                        new
                        {
                            Id = 2,
                            FirstRoomId = 3,
                            NameOfRoom = "Room 2",
                            SecondRoomId = 4,
                            ThirdRoomId = 5
                        },
                        new
                        {
                            Id = 3,
                            FirstRoomId = 4,
                            NameOfRoom = "Room 3",
                            SecondRoomId = 1,
                            ThirdRoomId = 4
                        },
                        new
                        {
                            Id = 4,
                            FirstRoomId = 6,
                            NameOfRoom = "Room 4",
                            SecondRoomId = 3,
                            ThirdRoomId = 5
                        },
                        new
                        {
                            Id = 5,
                            FirstRoomId = 4,
                            NameOfRoom = "Room 5",
                            SecondRoomId = 6,
                            ThirdRoomId = 7
                        },
                        new
                        {
                            Id = 6,
                            FirstRoomId = 4,
                            NameOfRoom = "Room 6",
                            SecondRoomId = 5,
                            ThirdRoomId = 7
                        },
                        new
                        {
                            Id = 7,
                            FirstRoomId = 9,
                            NameOfRoom = "Room 7",
                            SecondRoomId = 8,
                            ThirdRoomId = 6
                        },
                        new
                        {
                            Id = 8,
                            FirstRoomId = 6,
                            NameOfRoom = "Room 8",
                            SecondRoomId = 11,
                            ThirdRoomId = 10
                        },
                        new
                        {
                            Id = 9,
                            FirstRoomId = 17,
                            NameOfRoom = "Room 9",
                            SecondRoomId = 11,
                            ThirdRoomId = 7
                        },
                        new
                        {
                            Id = 10,
                            FirstRoomId = 8,
                            NameOfRoom = "Room 10",
                            SecondRoomId = 11,
                            ThirdRoomId = 12
                        },
                        new
                        {
                            Id = 11,
                            FirstRoomId = 9,
                            NameOfRoom = "Room 11",
                            SecondRoomId = 8,
                            ThirdRoomId = 11
                        },
                        new
                        {
                            Id = 12,
                            FirstRoomId = 10,
                            NameOfRoom = "Room 12",
                            SecondRoomId = 13,
                            ThirdRoomId = 14
                        },
                        new
                        {
                            Id = 13,
                            FirstRoomId = 16,
                            NameOfRoom = "Room 13",
                            SecondRoomId = 15,
                            ThirdRoomId = 14
                        },
                        new
                        {
                            Id = 14,
                            FirstRoomId = 12,
                            NameOfRoom = "Room 14",
                            SecondRoomId = 13,
                            ThirdRoomId = 15
                        },
                        new
                        {
                            Id = 15,
                            FirstRoomId = 17,
                            NameOfRoom = "Room 15",
                            SecondRoomId = 16,
                            ThirdRoomId = 14
                        },
                        new
                        {
                            Id = 16,
                            FirstRoomId = 17,
                            NameOfRoom = "Room 16",
                            SecondRoomId = 16,
                            ThirdRoomId = 14
                        },
                        new
                        {
                            Id = 17,
                            FirstRoomId = 16,
                            NameOfRoom = "Room 17",
                            SecondRoomId = 18,
                            ThirdRoomId = 15
                        },
                        new
                        {
                            Id = 18,
                            NameOfRoom = "You Won!!!"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
