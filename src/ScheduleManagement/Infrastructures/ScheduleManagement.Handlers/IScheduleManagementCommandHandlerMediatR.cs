using Framework.Commands.CommandHandlers;
using MediatR;

namespace ScheduleManagement.Handlers
{
    public interface
        IScheduleManagementCommandHandlerMediatR<in TOrderCommandRequest, TOrderCommandResponse> :
            ITransactionalCommandHandlerMediatR<TOrderCommandRequest, TOrderCommandResponse>
        where TOrderCommandResponse : ResponseCommand where TOrderCommandRequest : IRequest<TOrderCommandResponse>
    {
    }
}