using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleManagement.Persistence.EF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateOfBooking = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StartedBookingTime = table.Column<TimeSpan>(nullable: false),
                    EndedBookingTime = table.Column<TimeSpan>(nullable: false),
                    SubjectId = table.Column<string>(nullable: true),
                    BookingTimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingTimes_BookingDates_BookingTimeId",
                        column: x => x.BookingTimeId,
                        principalTable: "BookingDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTimes_BookingTimeId",
                table: "BookingTimes",
                column: "BookingTimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTimes");

            migrationBuilder.DropTable(
                name: "BookingDates");
        }
    }
}
