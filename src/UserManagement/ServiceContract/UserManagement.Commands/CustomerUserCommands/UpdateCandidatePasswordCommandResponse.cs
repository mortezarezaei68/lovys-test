using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class UpdateCandidatePasswordCommandResponse:ResponseCommand
    {
        public UpdateCandidatePasswordCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}