using UserManagement.Handlers;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class SendEmailToResetPasswordCommandRequest:IUserManagementRequest<SendEmailToResetPasswordCommandResponse>
    {
        public string Email { get; set; }
    }
}