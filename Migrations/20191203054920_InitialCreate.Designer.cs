﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TRAILES.Data;

namespace TRAILES.Migrations
{
    [DbContext(typeof(TRAILESContext))]
    [Migration("20191203054920_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1");

            modelBuilder.Entity("TRAILES.Models.Cabin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BedsFilled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Cabin");
                });

            modelBuilder.Entity("TRAILES.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxAttendance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("TRAILES.Models.EventAttendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Assigned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.HasIndex("StudentID");

                    b.ToTable("EventAttendance");
                });

            modelBuilder.Entity("TRAILES.Models.FacStaff", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Admin")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CabinID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMid")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CabinID");

                    b.ToTable("FacStaff");
                });

            modelBuilder.Entity("TRAILES.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CabinID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMidName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GradeLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("priorityRemaining")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CabinID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("TRAILES.Models.EventAttendance", b =>
                {
                    b.HasOne("TRAILES.Models.Event", "Event")
                        .WithMany("Attendances")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TRAILES.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TRAILES.Models.FacStaff", b =>
                {
                    b.HasOne("TRAILES.Models.Cabin", "Cabin")
                        .WithMany("Employees")
                        .HasForeignKey("CabinID");
                });

            modelBuilder.Entity("TRAILES.Models.Student", b =>
                {
                    b.HasOne("TRAILES.Models.Cabin", "Cabin")
                        .WithMany("Students")
                        .HasForeignKey("CabinID");
                });
#pragma warning restore 612, 618
        }
    }
}
