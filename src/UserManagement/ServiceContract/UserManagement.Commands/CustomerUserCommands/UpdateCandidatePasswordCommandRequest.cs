using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class UpdateCandidatePasswordCommandRequest:IUserManagementRequest<UpdateCandidatePasswordCommandResponse>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}