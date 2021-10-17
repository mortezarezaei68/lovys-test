﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleManagement.Persistence.EF.Context;

namespace ScheduleManagement.Persistence.EF.Migrations
{
    [DbContext(typeof(ScheduleManagementDbContext))]
    partial class ScheduleManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScheduleManagement.Domain.BookingDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBooking")
                        .HasColumnType("date");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BookingDates");
                });

            modelBuilder.Entity("ScheduleManagement.Domain.BookingDate", b =>
                {
                    b.OwnsMany("ScheduleManagement.Domain.BookingTime", "BookingTimes", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("BookingTimeId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<int>("CreatedBy")
                                .HasColumnType("int");

                            b1.Property<DateTime>("DeletedAt")
                                .HasColumnType("datetime2");

                            b1.Property<TimeSpan>("EndedBookingTime")
                                .HasColumnType("time");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<TimeSpan>("StartedBookingTime")
                                .HasColumnType("time");

                            b1.Property<string>("SubjectId")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<int>("UpdatedBy")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("BookingTimeId");

                            b1.ToTable("BookingTimes");

                            b1.WithOwner()
                                .HasForeignKey("BookingTimeId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
