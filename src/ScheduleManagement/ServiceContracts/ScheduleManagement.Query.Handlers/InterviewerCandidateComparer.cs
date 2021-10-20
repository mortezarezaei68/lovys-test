using System.Collections.Generic;
using ScheduleManagement.Domain;

namespace ScheduleManagement.Query.Handlers
{
    internal class InterviewerCandidateComparer : IEqualityComparer<BookingDateBookingTimeView>
    {
        public bool Equals(BookingDateBookingTimeView view, BookingDateBookingTimeView timeView)
        {
            if (view.EndedBookingTime == timeView.EndedBookingTime && view.StartedBookingTime == timeView.StartedBookingTime &&
                view.DateOfBooking == timeView.DateOfBooking && view.SubjectId != timeView.SubjectId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(BookingDateBookingTimeView obj)
        {
            return obj.DateOfBooking.GetHashCode();
        }
    }
}