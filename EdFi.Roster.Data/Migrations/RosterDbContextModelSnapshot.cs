﻿// <auto-generated />
using System;
using EdFi.Roster.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdFi.Roster.Data.Migrations
{
    [DbContext(typeof(RosterDbContext))]
    partial class RosterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("EdFi.Roster.Models.ApiLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Method")
                        .HasColumnType("TEXT");

                    b.Property<string>("StatusCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Uri")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ApiLogEntries");
                });

            modelBuilder.Entity("EdFi.Roster.Models.ApiSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("RootUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Secret")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ApiSettings");
                });

            modelBuilder.Entity("EdFi.Roster.Models.RosterLocalEducationAgency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LocalEducationAgencies");
                });

            modelBuilder.Entity("EdFi.Roster.Models.RosterSchool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("EdFi.Roster.Models.RosterSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("EdFi.Roster.Models.RosterStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("EdFi.Roster.Models.RosterStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
