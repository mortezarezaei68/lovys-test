using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Queries;
using MediatR;

namespace ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate
{
    public class GetAllUserScheduleQueryRequest:IRequest<GetAllUserScheduleQueryResponse>
    {
        
    }

    public class GetAllUserScheduleQueryResponse:ResponseQuery<IEnumerable<InterviewerScheduleQueryModel>>
    {
        public GetAllUserScheduleQueryResponse(bool isSuccess, IEnumerable<InterviewerScheduleQueryModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }

    public class InterviewerScheduleQueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan StartedDate { get; set; }
        public TimeSpan EndedDate { get; set; }
        public DateTime BookingDate { get; set; }
        public IEnumerable<CandidateScheduleQueryModel> CandidateSchedule { get; set; }
    }

    public class CandidateScheduleQueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan StartedDate { get; set; }
        public TimeSpan EndedDate { get; set; }
    }
}