using System;

namespace ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate
{
    public class CandidateScheduleQueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan StartedDate { get; set; }
        public TimeSpan EndedDate { get; set; }
    }
}