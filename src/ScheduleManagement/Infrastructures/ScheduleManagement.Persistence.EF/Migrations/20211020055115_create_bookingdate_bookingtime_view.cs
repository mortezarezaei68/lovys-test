using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleManagement.Persistence.EF.Migrations
{
    public partial class create_bookingdate_bookingtime_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				               CREATE VIEW vwBookingDateAndBookingTime AS
				                    SELECT bd.DateOfBooking,
					                       bt.StartedBookingTime,
					                       bt.EndedBookingTime,
					                       us.FirstName,
					                       us.LastName,
					                       us.UserType,
					                       us.Email,
					                       us.SubjectId
					                FROM [InterviewApplication].dbo.BookingDates AS bd
					                LEFT JOIN [InterviewApplication].dbo.BookingTimes AS bt ON bd.Id=bt.BookingDateId
					                LEFT JOIN [InterviewApplication].dbo.AspNetUsers AS us ON us.SubjectId = bt.SubjectId
                					WHERE bt.IsDeleted <> 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
drop view vwBookingDateAndBookingTime;
");
        }
    }
}
