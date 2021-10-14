using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.UserRoleCommands
{
    public class AddRoleCommandResponse:ResponseCommand
    {
        public AddRoleCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}