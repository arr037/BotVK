﻿// <auto-generated />
using System;
using Api.Engine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200130054815_AddGroupId")]
    partial class AddGroupId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Models.AboutTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cabinet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Teacher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimeTableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeTableId");

                    b.ToTable("AboutTime");
                });

            modelBuilder.Entity("Api.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Api.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimeTableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeTableId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Api.Models.TimeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Weekday")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TimeTable");
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSignedGroup")
                        .HasColumnType("bit");

                    b.Property<int?>("TimeTableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("TimeTableId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api.Models.AboutTime", b =>
                {
                    b.HasOne("Api.Models.TimeTable", null)
                        .WithMany("AboutTime")
                        .HasForeignKey("TimeTableId");
                });

            modelBuilder.Entity("Api.Models.Admin", b =>
                {
                    b.HasOne("Api.Models.Group", null)
                        .WithMany("Admins")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Api.Models.Group", b =>
                {
                    b.HasOne("Api.Models.TimeTable", "TimeTable")
                        .WithMany()
                        .HasForeignKey("TimeTableId");
                });

            modelBuilder.Entity("Api.Models.User", b =>
                {
                    b.HasOne("Api.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.TimeTable", "TimeTable")
                        .WithMany()
                        .HasForeignKey("TimeTableId");
                });
#pragma warning restore 612, 618
        }
    }
}
