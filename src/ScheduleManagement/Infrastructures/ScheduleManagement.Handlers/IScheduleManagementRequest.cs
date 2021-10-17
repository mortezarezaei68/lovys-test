using Framework.Commands.CommandHandlers;
using MediatR;

namespace ScheduleManagement.Handlers
{
    public interface IScheduleManagementRequest<out TResponseCommand>:IRequest<TResponseCommand> where TResponseCommand:ResponseCommand
    {
        
    }
}