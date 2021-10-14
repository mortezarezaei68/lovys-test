using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class SignOutAdminUserCommandRequest:IUserManagementRequest<SignOutAdminUserCommandResponse>
    {
        
    }

    public class SignOutAdminUserCommandResponse:ResponseCommand
    {
        public SignOutAdminUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}