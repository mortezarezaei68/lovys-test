using System.Threading;
using System.Threading.Tasks;
using Framework.Query;
using ScheduleManagement.Query.Commands.GetAllScheduleInterviewCandidate;

namespace ScheduleManagement.Query.Handlers
{
    public interface IGetAllUserScheduleQueryHandler:IQueryHandler
    {
        Task<GetAllUserScheduleQueryResponse> Handle(GetAllUserScheduleQueryRequest request,CancellationToken cancellationToken);
    }
}