using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.UserTokenCommands
{
    public class AddPersistGrantsCommandResponse:ResponseCommand
    {
        public AddPersistGrantsCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
        {
        }
    }
}