using MediatR;
using UserManagement.Handlers;

namespace UserManagement.Commands.UserRoleCommands
{
    public class AddRoleCommandRequest:IUserManagementRequest<AddRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}