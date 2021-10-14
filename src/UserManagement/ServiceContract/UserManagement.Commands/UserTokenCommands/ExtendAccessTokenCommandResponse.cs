using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace UserManagement.Commands.UserTokenCommands
{
    public class ExtendAccessTokenCommandResponse:ResponseCommand<ExtendRefreshTokenViewModel>
    {
        public ExtendAccessTokenCommandResponse(bool isSuccess, ResultCode resultCode, ExtendRefreshTokenViewModel data, string message = null) : base(isSuccess, resultCode, data, message)
        {
        }
    }

    public class ExtendRefreshTokenViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}