using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class AddCandidateCommandResponse:ResponseCommand
    {
        public AddCandidateCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}