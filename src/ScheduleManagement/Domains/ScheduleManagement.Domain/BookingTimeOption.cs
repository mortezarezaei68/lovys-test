using System;

namespace ScheduleManagement.Domain
{
    public class BookingTimeOption
    {
        public TimeSpan StartedTime { get; set; }
        public TimeSpan EndedTime { get; set; }
    }
}