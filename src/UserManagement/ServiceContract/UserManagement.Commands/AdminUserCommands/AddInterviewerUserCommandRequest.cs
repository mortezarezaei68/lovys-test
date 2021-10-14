using System;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class AddInterviewerUserCommandRequest:IUserManagementRequest<AddInterviewerUserCommandResponse>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}