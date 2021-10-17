using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Domain.Core;
using Framework.Exception.Exceptions;

namespace ScheduleManagement.Domain
{
    public class BookingDate : AggregateRoot<int>
    {
        public DateTime DateOfBooking { get; private set; }
        private readonly List<BookingTime> _bookingTimes = new();
        public IReadOnlyCollection<BookingTime> BookingTimes => _bookingTimes;
        public static BookingDate Add(DateTime dateOfBooking, TimeSpan startedBookingTime, TimeSpan endedBookingTime,
            string subjectId)
        {
            var bookingDate = new BookingDate
            {
                DateOfBooking = dateOfBooking
            };
            bookingDate._bookingTimes.Add(BookingTime.Add(startedBookingTime, endedBookingTime, subjectId));
            return bookingDate;
        }

        public void Update(TimeSpan startedBookingTime, TimeSpan endedBookingTime,
            string subjectId)
        {
            var bookingTime = _bookingTimes.FirstOrDefault(a => a.SubjectId == subjectId);
            if (bookingTime is null)
            {
                _bookingTimes.Add(BookingTime.Add(startedBookingTime, endedBookingTime, subjectId));
            }
            else
            {
                var existBookingTime = _bookingTimes.Where(a =>
                    a.SubjectId == subjectId &&
                    a.StartedBookingTime.IsBetween(startedBookingTime, endedBookingTime) ||
                    a.EndedBookingTime.IsBetween(startedBookingTime, endedBookingTime));
                foreach (var time in existBookingTime)
                {
                    time.Delete();
                }

                _bookingTimes.Add(BookingTime.Add(startedBookingTime, endedBookingTime, subjectId));
            }
        }
        
        



    }
}