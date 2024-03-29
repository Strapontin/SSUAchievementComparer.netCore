﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSUAchievementComparer.Data;

namespace SSUAchievementComparer.Data.Migrations
{
    [DbContext(typeof(SSUAchievementComparerDbContext))]
    [Migration("20211222090527_AddedIndexOnGameName")]
    partial class AddedIndexOnGameName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SSUAchievementComparer.Core.Entities.DB.GameDetailsDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("appid")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("name")
                        .HasAnnotation("SqlServer:Include", new[] { "appid" });

                    b.ToTable("GameDetailsDb");
                });

            modelBuilder.Entity("SSUAchievementComparer.Core.Entities.DB.PlayerDetailsDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("playerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("PlayerDetailsDb");
                });
#pragma warning restore 612, 618
        }
    }
}
