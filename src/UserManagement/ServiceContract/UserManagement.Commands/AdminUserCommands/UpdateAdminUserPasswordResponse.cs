using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.AdminUserCommands
{
    public class UpdateAdminUserPasswordResponse:ResponseCommand
    {
        public UpdateAdminUserPasswordResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}