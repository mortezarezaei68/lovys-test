using System;

namespace ScheduleManagement.Validations
{
    public interface IBookingDateValidationService
    {
        void CheckValidTimeRange(TimeSpan startedDate, TimeSpan endedDate);
    }
}