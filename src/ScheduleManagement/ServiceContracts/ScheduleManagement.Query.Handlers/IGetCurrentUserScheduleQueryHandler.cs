using System.Threading;
using System.Threading.Tasks;
using Framework.Query;
using ScheduleManagement.Query.Commands;

namespace ScheduleManagement.Query.Handlers
{
    public interface IGetCurrentUserScheduleQueryHandler:IQueryHandler
    {
        Task<GetCurrentInterviewerCandidateScheduleQueryResponse> Handle(GetCurrentInterviewerCandidateScheduleQueryRequest request,CancellationToken cancellationToken);
    }
}