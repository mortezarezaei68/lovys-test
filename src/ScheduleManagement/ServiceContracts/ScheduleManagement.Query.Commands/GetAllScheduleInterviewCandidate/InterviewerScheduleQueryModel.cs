using System;
using System.Collections.Generic;

namespace ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate
{
    public class InterviewerScheduleQueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan StartedDate { get; set; }
        public TimeSpan EndedDate { get; set; }
        public DateTime BookingDate { get; set; }
        public IEnumerable<CandidateScheduleQueryModel> CandidateSchedule { get; set; }
    }
}