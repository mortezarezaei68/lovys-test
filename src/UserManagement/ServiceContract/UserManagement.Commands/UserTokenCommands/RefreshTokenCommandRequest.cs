using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Commands.UserTokenCommands
{
    public class RefreshTokenCommandRequest:IUserManagementRequest<RefreshTokenCommandResponse>
    {
        public UserType UserType { get; set; }
        public string SubjectId { get; set; }
        public int UserId { get; set; }
    }
}