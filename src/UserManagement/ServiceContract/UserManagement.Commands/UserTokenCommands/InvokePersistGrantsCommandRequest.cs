using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using UserManagement.Handlers;

namespace UserManagement.Commands.UserTokenCommands
{
    public class InvokePersistGrantsCommandRequest:IUserManagementRequest<InvokePersistGrantsCommandResponse>
    {
        public string UserId { get; set; }
    }

    public class InvokePersistGrantsCommandResponse:ResponseCommand
    {
        public InvokePersistGrantsCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}