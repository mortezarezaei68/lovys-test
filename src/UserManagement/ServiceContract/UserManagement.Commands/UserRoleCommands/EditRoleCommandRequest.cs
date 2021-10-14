using UserManagement.Handlers;

namespace UserManagement.Commands.UserRoleCommands
{
    public class EditRoleCommandRequest:IUserManagementRequest<EditRoleCommandResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}