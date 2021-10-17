using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ScheduleManagement.Commands
{
    public class AddBookingDateTimeCommandResponse : ResponseCommand
    {
        public AddBookingDateTimeCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess,
            resultCode, message)
        {
        }
    }
}