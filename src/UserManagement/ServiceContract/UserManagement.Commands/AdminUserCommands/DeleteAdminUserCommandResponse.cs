using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.AdminUserCommands
{
    public class DeleteAdminUserCommandResponse:ResponseCommand
    {
        public DeleteAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}