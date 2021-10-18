using MediatR;

namespace ScheduleManagement.Query.Commands
{
    public class GetCurrentInterviewerCandidateScheduleQueryRequest:IRequest<GetCurrentInterviewerCandidateScheduleQueryResponse>
    {
    }
}