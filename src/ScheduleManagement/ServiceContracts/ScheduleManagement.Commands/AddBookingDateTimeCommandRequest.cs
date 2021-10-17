using System;
using System.Collections.Generic;
using ScheduleManagement.Handlers;

namespace ScheduleManagement.Commands
{
    public class AddBookingDateTimeCommandRequest : IScheduleManagementRequest<AddBookingDateTimeCommandResponse>
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan StartedTime { get; set; }
        public TimeSpan EndedTime { get; set; }
    }
}