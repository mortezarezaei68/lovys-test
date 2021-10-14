using UserManagement.Handlers;

namespace UserManagement.Commands.UserRoleCommands
{
    public class AddRoleToUserCommandRequest:IUserManagementRequest<AddRoleToUserCommandResponse>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}