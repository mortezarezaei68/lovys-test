using System.Collections.Generic;
using System.Linq;
using Framework.Query;

namespace ScheduleManagement.Query.Commands
{
    public class GetCurrentInterviewerCandidateScheduleQueryResponse:ResponseQuery<IEnumerable<ScheduleQueryModel>>
    {
        public GetCurrentInterviewerCandidateScheduleQueryResponse(bool isSuccess, IEnumerable<ScheduleQueryModel> data, int count = 1, string message = null) : base(isSuccess, data, data.Count(), message)
        {
        }
    }
}