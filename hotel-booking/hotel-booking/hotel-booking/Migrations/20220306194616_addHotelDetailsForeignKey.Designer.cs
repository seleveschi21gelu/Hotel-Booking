﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hotel_booking.DAL;

#nullable disable

namespace hotel_booking.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20220306194616_addHotelDetailsForeignKey")]
    partial class addHotelDetailsForeignKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("hotel_booking.DAL.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeesNumber")
                        .HasColumnType("int");

                    b.Property<int?>("HotelDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelDetailsId")
                        .IsUnique()
                        .HasFilter("[HotelDetailsId] IS NOT NULL");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("hotel_booking.DAL.Models.HotelDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Rooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Stars")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("HotelDetails");
                });

            modelBuilder.Entity("hotel_booking.DAL.Models.Pictures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HotelDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelDetailsId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("hotel_booking.DAL.Models.Hotel", b =>
                {
                    b.HasOne("hotel_booking.DAL.Models.HotelDetails", "HotelDetails")
                        .WithOne("Hotels")
                        .HasForeignKey("hotel_booking.DAL.Models.Hotel", "HotelDetailsId");

                    b.Navigation("HotelDetails");
                });

            modelBuilder.Entity("hotel_booking.DAL.Models.Pictures", b =>
                {
                    b.HasOne("hotel_booking.DAL.Models.HotelDetails", "HotelDetails")
                        .WithMany("Pictures")
                        .HasForeignKey("HotelDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelDetails");
                });

            modelBuilder.Entity("hotel_booking.DAL.Models.HotelDetails", b =>
                {
                    b.Navigation("Hotels")
                        .IsRequired();

                    b.Navigation("Pictures");
                });
#pragma warning restore 612, 618
        }
    }
}