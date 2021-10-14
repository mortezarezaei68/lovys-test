using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class DeleteCandidateAddressCommandResponse : ResponseCommand
    {
        public DeleteCandidateAddressCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}