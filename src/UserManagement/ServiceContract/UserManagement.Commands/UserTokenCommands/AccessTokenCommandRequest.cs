using System.Collections.Generic;
using UserManagement.Domain;
using UserManagement.Handlers;

namespace UserManagement.Commands.UserTokenCommands
{
    public class AccessTokenCommandRequest:IUserManagementRequest<AccessTokenCommandResponse>
    {
        public string SubjectId { get; set; }
        public List<string> Roles { get; set; }
        public UserType UserType { get; set; }
        public int UserId { get; set; }
    }
}