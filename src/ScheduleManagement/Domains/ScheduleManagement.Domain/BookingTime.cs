using System;
using Framework.Domain.Core;

namespace ScheduleManagement.Domain
{
    public class BookingTime : Entity<int>
    {
        public static BookingTime Add(TimeSpan startedBookingTime, TimeSpan endedBookingTime, string subjectId)
        {
            return new()
            {
                StartedBookingTime = startedBookingTime,
                EndedBookingTime = endedBookingTime,
                SubjectId = subjectId,
            };
        }

        public TimeSpan StartedBookingTime { get; private set; }
        public TimeSpan EndedBookingTime { get; private set; }
        public string SubjectId { get; private set; } }
}