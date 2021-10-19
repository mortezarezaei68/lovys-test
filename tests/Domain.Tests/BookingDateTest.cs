using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ScheduleManagement.Domain;
using UserManagement.Domain;
using Xunit;

namespace Domain.Tests
{
    public class BookingDateTest
    {
        [Fact]
        public void Should_create_booking_date()
        {
            DateTime dateOfBooking = DateTime.Now;
            var bookingOption = new List<BookingTimeOption>()
            {
                new ()
                {
                    EndedTime = new TimeSpan(14, 0, 0),
                    StartedTime = new TimeSpan(13, 0, 0)
                }
            };
            string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            var bookingDate = BookingDate.Add(dateOfBooking,subjectId, bookingOption );
            bookingDate.DateOfBooking.Should().Be(dateOfBooking);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
            {
                first.SubjectId.Should().Be(subjectId);
                first.EndedBookingTime.Should().Be(bookingOption.First().EndedTime);
                first.StartedBookingTime.Should().Be(bookingOption.First().StartedTime);
                first.SubjectId.Should().Be(subjectId);
            });
        }

        [Fact]
        public void Should_update_booking_date()
        {
            DateTime dateOfBooking = DateTime.Now;
            var bookingOption = new List<BookingTimeOption>()
            {
                new ()
                {
                    EndedTime = new TimeSpan(14, 0, 0),
                    StartedTime = new TimeSpan(13, 0, 0)
                }
            };
            string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            var bookingDate = BookingDate.Add(dateOfBooking, subjectId,bookingOption);
            bookingDate.DateOfBooking.Should().Be(dateOfBooking);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
            {
                first.SubjectId.Should().Be(subjectId);
                first.EndedBookingTime.Should().Be(bookingOption.First().EndedTime);
                first.StartedBookingTime.Should().Be(bookingOption.First().StartedTime);
                first.SubjectId.Should().Be(subjectId);
            });
        }

        [Fact]
        public void Should_update_an_booking_time()
        {
            var bookingDate = GetBookingDate();
            var secondBookingDate=ObjectDate.Last();
            const string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            bookingDate.Update(ObjectDate, subjectId);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
                {
                    first.Id.Should().Be(1);
                    first.EndedBookingTime.Should().Be(ObjectDate.First().EndedTime);
                    first.StartedBookingTime.Should().Be(ObjectDate.First().StartedTime);
                    first.SubjectId.Should().Be(subjectId);

                },
                second =>
                {
                    second.SubjectId.Should().Be(subjectId);
                    second.EndedBookingTime.Should().Be(secondBookingDate.EndedTime);
                    second.StartedBookingTime.Should().Be(secondBookingDate.StartedTime);
                });
        }


        public static IEnumerable<BookingTimeOption> ObjectDate =>
            new List<BookingTimeOption>
            {
                new BookingTimeOption
                {
                    StartedTime = new TimeSpan(13, 0, 0),
                    EndedTime = new TimeSpan(14, 0, 0),
                },
                new BookingTimeOption
                {
                    StartedTime = new TimeSpan(14, 0, 0),
                    EndedTime = new TimeSpan(15, 0, 0),
                },
            };

        private static BookingDate GetBookingDate()
        {
            var firstBookingDate = ObjectDate.First();
            const string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            var bookingDate = BookingDate.Add(DateTime.Now, subjectId,ObjectDate );
            bookingDate.BookingTimes.First().SetId(1);
            return bookingDate;
        }
    }
}