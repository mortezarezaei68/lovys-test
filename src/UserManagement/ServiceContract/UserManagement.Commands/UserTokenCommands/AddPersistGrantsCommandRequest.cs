using System;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Commands.UserTokenCommands
{
    public class AddPersistGrantsCommandRequest:IUserManagementRequest<AddPersistGrantsCommandResponse>
    {
        public int UserId { get; set; }
        public string SubjectId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string IpAddress { get; set; }
        public UserType UserType { get; set; }
    }
}