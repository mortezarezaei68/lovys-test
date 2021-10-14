using UserManagement.Handlers;

namespace UserManagement.Commands.UserTokenCommands
{
    public class ExtendAccessTokenCommandRequest:IUserManagementRequest<ExtendAccessTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}