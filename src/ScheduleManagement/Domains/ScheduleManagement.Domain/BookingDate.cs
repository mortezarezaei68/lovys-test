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
        public static BookingDate Add(DateTime dateOfBooking,string subjectId, IEnumerable<BookingTimeOption> bookingTimes)
        {
            var bookingDate = new BookingDate
            {
                DateOfBooking = dateOfBooking
            };
            foreach (var bookingTime in bookingTimes)
            {
                bookingDate._bookingTimes.Add(BookingTime.Add(bookingTime.StartedTime, bookingTime.EndedTime, subjectId));
            }
           
            return bookingDate;
        }

        public void Update(IEnumerable<BookingTimeOption> bookingTimes,string subjectId)
        {
            var bookingTime = _bookingTimes.FirstOrDefault(a => a.SubjectId == subjectId);
            if (bookingTime is null)
            {
                foreach (var bookingTimeOption in bookingTimes)
                {
                    _bookingTimes.Add(BookingTime.Add(bookingTimeOption.StartedTime, bookingTimeOption.EndedTime, subjectId));
                }
            }
            else
            {
                foreach (var bookingTimeOption in bookingTimes)
                {
                    var existOverlap = _bookingTimes.Where(a =>
                        a.SubjectId == subjectId &&
                        a.StartedBookingTime.IsOverlap(bookingTimeOption.StartedTime, bookingTimeOption.EndedTime) ||
                        a.EndedBookingTime.IsOverlap(bookingTimeOption.StartedTime, bookingTimeOption.EndedTime));
                    foreach (var time in existOverlap)
                    {
                        time.Delete();
                    }
                    var existBookingTime = _bookingTimes.FirstOrDefault(a =>
                        a.SubjectId == subjectId && a.StartedBookingTime == bookingTimeOption.StartedTime &&
                        a.EndedBookingTime == bookingTimeOption.EndedTime);
                    if (existBookingTime is null)
                    {
                        _bookingTimes.Add(BookingTime.Add(bookingTimeOption.StartedTime, bookingTimeOption.EndedTime,
                            subjectId));  
                    }

     
                }
                
            }
        }
        
        



    }
}