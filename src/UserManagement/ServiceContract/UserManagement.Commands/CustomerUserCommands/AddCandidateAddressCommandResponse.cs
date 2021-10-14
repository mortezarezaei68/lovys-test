using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.CustomerUserCommands
{
    public class AddCandidateAddressCommandResponse : ResponseCommand
    {
        public AddCandidateAddressCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}