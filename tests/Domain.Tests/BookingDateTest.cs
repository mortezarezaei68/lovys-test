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
            TimeSpan startedBookingTime = new TimeSpan(13, 0, 0);
            TimeSpan endedBookingTime = new TimeSpan(14, 0, 0);
            string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            var bookingDate = BookingDate.Add(dateOfBooking, startedBookingTime, endedBookingTime, subjectId);
            bookingDate.DateOfBooking.Should().Be(dateOfBooking);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
            {
                first.SubjectId.Should().Be(subjectId);
                first.EndedBookingTime.Should().Be(endedBookingTime);
                first.StartedBookingTime.Should().Be(startedBookingTime);
                first.SubjectId.Should().Be(subjectId);
            });
        }

        [Fact]
        public void Should_update_booking_date()
        {
            DateTime dateOfBooking = DateTime.Now;
            TimeSpan startedBookingTime = new TimeSpan(13, 0, 0);
            TimeSpan endedBookingTime = new TimeSpan(14, 0, 0);
            string subjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C";
            var bookingDate = BookingDate.Add(dateOfBooking, startedBookingTime, endedBookingTime, subjectId);
            bookingDate.DateOfBooking.Should().Be(dateOfBooking);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
            {
                first.SubjectId.Should().Be(subjectId);
                first.EndedBookingTime.Should().Be(endedBookingTime);
                first.StartedBookingTime.Should().Be(startedBookingTime);
                first.SubjectId.Should().Be(subjectId);
            });
        }

        [Fact]
        public void Should_update_an_external_organization()
        {
            var bookingDate = GetBookingDate();
            var secondBookingDate=ObjectDate.Last();
            bookingDate.Update(secondBookingDate.StartedTime, secondBookingDate.EndedTime, secondBookingDate.SubjectId);

            bookingDate.BookingTimes.Should().SatisfyRespectively(first =>
                {
                    first.Id.Should().Be(1);
                    first.EndedBookingTime.Should().Be(ObjectDate.First().EndedTime);
                    first.StartedBookingTime.Should().Be(ObjectDate.First().StartedTime);
                    first.SubjectId.Should().Be(ObjectDate.First().SubjectId);

                },
                second =>
                {
                    second.SubjectId.Should().Be(secondBookingDate.SubjectId);
                    second.EndedBookingTime.Should().Be(secondBookingDate.EndedTime);
                    second.StartedBookingTime.Should().Be(secondBookingDate.StartedTime);
                });
        }


        public static IEnumerable<AddBookingDateTime> ObjectDate =>
            new List<AddBookingDateTime>
            {
                new AddBookingDateTime
                {
                    BookingDate = DateTime.Now,
                    StartedTime = new TimeSpan(13, 0, 0),
                    EndedTime = new TimeSpan(14, 0, 0),
                    SubjectId = "940FAD00-8D71-4092-BCE7-9B72D843F72C"
                },
                new AddBookingDateTime
                {
                    BookingDate = DateTime.Now,
                    StartedTime = new TimeSpan(15, 0, 0),
                    EndedTime = new TimeSpan(18, 0, 0),
                    SubjectId = "F7F9D597-96ED-499E-B488-7B94A416DA81"
                },
            };

        private static BookingDate GetBookingDate()
        {
            var firstBookingDate = ObjectDate.First();
            var bookingDate = BookingDate.Add(firstBookingDate.BookingDate, firstBookingDate.StartedTime,
                firstBookingDate.EndedTime, firstBookingDate.SubjectId);
            bookingDate.BookingTimes.First().SetId(1);
            return bookingDate;
        }

        public class AddBookingDateTime
        {
            public DateTime BookingDate { get; set; }
            public TimeSpan StartedTime { get; set; }
            public TimeSpan EndedTime { get; set; }
            public string SubjectId { get; set; }
        }
    }
}