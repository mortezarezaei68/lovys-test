using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class ConfirmResetPasswordCommandRequest:IUserManagementRequest<ConfirmResetPasswordCommandResponse>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}