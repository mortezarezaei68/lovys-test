using UserManagement.Handlers;

namespace UserManagement.Commands.CandidateUserCommands
{
    public class AddCandidateCommandRequest:IUserManagementRequest<AddCandidateCommandResponse>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
    }
}