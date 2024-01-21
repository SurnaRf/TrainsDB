﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(TrainDbContext))]
    [Migration("20240121160104_root")]
    partial class root
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessLayer.Connection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NodeAId")
                        .HasColumnType("int");

                    b.Property<int>("NodeBId")
                        .HasColumnType("int");

                    b.Property<string>("TerrainType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NodeAId");

                    b.HasIndex("NodeBId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("BusinessLayer.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Coordinates")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("BusinessLayer.Locomotive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("CarryingCapacity")
                        .HasColumnType("float");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("LocomotiveType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("TrainCompositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TrainCompositionId");

                    b.ToTable("Locomotives");
                });

            modelBuilder.Entity("BusinessLayer.TrainCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("TrainCarType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrainCompositionId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TrainCompositionId");

                    b.ToTable("TrainCars");
                });

            modelBuilder.Entity("BusinessLayer.TrainComposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("TrainCompositions");
                });

            modelBuilder.Entity("BusinessLayer.Connection", b =>
                {
                    b.HasOne("BusinessLayer.Location", "NodeA")
                        .WithMany("ConnectionsA")
                        .HasForeignKey("NodeAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessLayer.Location", "NodeB")
                        .WithMany("ConnectionsB")
                        .HasForeignKey("NodeBId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NodeA");

                    b.Navigation("NodeB");
                });

            modelBuilder.Entity("BusinessLayer.Locomotive", b =>
                {
                    b.HasOne("BusinessLayer.Location", "Location")
                        .WithMany("Locomotives")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLayer.TrainComposition", "TrainComposition")
                        .WithMany("Locomotives")
                        .HasForeignKey("TrainCompositionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Location");

                    b.Navigation("TrainComposition");
                });

            modelBuilder.Entity("BusinessLayer.TrainCar", b =>
                {
                    b.HasOne("BusinessLayer.Location", "Location")
                        .WithMany("TrainCars")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLayer.TrainComposition", "TrainComposition")
                        .WithMany("TrainCars")
                        .HasForeignKey("TrainCompositionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Location");

                    b.Navigation("TrainComposition");
                });

            modelBuilder.Entity("BusinessLayer.TrainComposition", b =>
                {
                    b.HasOne("BusinessLayer.Location", "Location")
                        .WithMany("TrainCompositions")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("BusinessLayer.Location", b =>
                {
                    b.Navigation("ConnectionsA");

                    b.Navigation("ConnectionsB");

                    b.Navigation("Locomotives");

                    b.Navigation("TrainCars");

                    b.Navigation("TrainCompositions");
                });

            modelBuilder.Entity("BusinessLayer.TrainComposition", b =>
                {
                    b.Navigation("Locomotives");

                    b.Navigation("TrainCars");
                });
#pragma warning restore 612, 618
        }
    }
}
