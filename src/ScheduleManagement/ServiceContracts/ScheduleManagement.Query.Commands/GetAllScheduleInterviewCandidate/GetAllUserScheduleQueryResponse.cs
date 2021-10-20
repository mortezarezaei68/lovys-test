using System.Collections.Generic;
using System.Linq;
using Framework.Query;

namespace ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate
{
    public class GetAllUserScheduleQueryResponse:ResponseQuery<IEnumerable<InterviewerScheduleQueryModel>>
    {
        public GetAllUserScheduleQueryResponse(bool isSuccess, IEnumerable<InterviewerScheduleQueryModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }
}