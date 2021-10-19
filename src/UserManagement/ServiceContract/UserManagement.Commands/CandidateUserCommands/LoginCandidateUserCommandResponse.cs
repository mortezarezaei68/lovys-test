using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.CandidateUserCommands
{
    public class LoginCandidateUserCommandResponse:ResponseCommand
    {
        public LoginCandidateUserCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}