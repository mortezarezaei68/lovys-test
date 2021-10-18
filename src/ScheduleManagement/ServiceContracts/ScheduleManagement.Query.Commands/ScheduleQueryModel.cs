using System;
using System.Collections.Generic;

namespace ScheduleManagement.Query.Commands
{
    public class ScheduleQueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BookingDate { get; set; }
        public IEnumerable<SubScheduleQueryModel> SubSchedules { get; set; }
    }
}