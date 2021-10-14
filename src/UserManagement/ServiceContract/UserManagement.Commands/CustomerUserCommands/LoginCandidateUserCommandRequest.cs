using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class LoginCandidateUserCommandRequest:IUserManagementRequest<LoginCandidateUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}