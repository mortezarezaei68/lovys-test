using Framework.Commands.CommandHandlers;
using MediatR;

namespace ScheduleManagement.Handlers
{
    public interface
        IScheduleManagementCommandHandlerMediatR<in TScheduleCommandRequest, TScheduleCommandResponse> :
            ITransactionalCommandHandlerMediatR<TScheduleCommandRequest, TScheduleCommandResponse>
        where TScheduleCommandResponse : ResponseCommand where TScheduleCommandRequest : IRequest<TScheduleCommandResponse>
    {
    }
}