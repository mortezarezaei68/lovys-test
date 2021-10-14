using UserManagement.Handlers;

namespace UserManagement.Commands.UserRoleCommands
{
    public class DeleteRoleCommandRequest:IUserManagementRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}