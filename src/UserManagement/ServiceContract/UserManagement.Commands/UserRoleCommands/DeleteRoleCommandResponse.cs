using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.UserRoleCommands
{
    public class DeleteRoleCommandResponse:ResponseCommand
    {
        public DeleteRoleCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}