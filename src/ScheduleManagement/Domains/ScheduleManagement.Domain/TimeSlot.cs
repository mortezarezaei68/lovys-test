using System;

namespace ScheduleManagement.Domain
{
    public static class TimeSlot
    {
        public static bool IsOverlap(this TimeSpan selected, TimeSpan start, TimeSpan end)
        {
            return selected >= start && selected <= end;
        }

        public static bool IsBetween(this TimeSpan candidateStarted, TimeSpan candidateEnded, TimeSpan interviewStarted, TimeSpan interviewEnded)
        {
            return candidateEnded <= interviewEnded && candidateStarted >= interviewStarted;
        }
    }
}