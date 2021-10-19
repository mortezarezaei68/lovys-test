using System;

namespace ScheduleManagement.Domain
{
    public class BookingDateBookingTimeView
    {
        public DateTime DateOfBooking { get; private set; }
        public TimeSpan StartedBookingTime { get; private set; }
        public TimeSpan EndedBookingTime { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public int UserType { get; private set; }
        public Guid SubjectId { get; private set; }
    }
}