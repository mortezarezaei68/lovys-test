using MediatR;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class DeleteInterviewerUserCommandRequest:IUserManagementRequest<DeleteAdminUserCommandResponse>
    {
        public string Id { get; set; }
    }
}