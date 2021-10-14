using System;
using UserManagement.Handlers;

namespace UserManagement.Commands.AdminUserCommands
{
    public class EditInterviewerUserCommandRequest:IUserManagementRequest<EditAdminUserCommandResponse>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

    }
}