using MediatR;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class UpdateAdminUserPasswordRequest:IUserManagementRequest<UpdateAdminUserPasswordResponse>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        
    }
}