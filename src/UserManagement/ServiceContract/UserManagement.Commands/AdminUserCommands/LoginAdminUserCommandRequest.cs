using MediatR;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class LoginAdminUserCommandRequest:IUserManagementRequest<LoginAdminUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}