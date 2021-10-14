using Framework.Commands.CommandHandlers;
using MediatR;

namespace UserManagement.Handlers
{
    public interface IUserManagementRequest<out TUserResponseCommand>:IRequest<TUserResponseCommand> where TUserResponseCommand:ResponseCommand
    {
        
    }
}