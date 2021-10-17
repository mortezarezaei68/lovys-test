using System;

namespace ScheduleManagement.Domain
{
    public static class TimeSlot
    {
        public static bool IsBetween(this TimeSpan selected, TimeSpan start, TimeSpan end)
        {
            return selected >= start && selected <= end;
        }
    }
}