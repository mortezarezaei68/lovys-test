using System;

namespace ScheduleManagement.Query.Commands
{
    public class SubScheduleQueryModel
    {
        public TimeSpan EndedDate { get; set; }
        public TimeSpan StartedDate { get; set; }
    }
}