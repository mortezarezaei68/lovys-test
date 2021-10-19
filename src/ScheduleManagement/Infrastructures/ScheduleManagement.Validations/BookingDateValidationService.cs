using System;
using Framework.Exception.Exceptions;

namespace ScheduleManagement.Validations
{
    public class BookingDateValidationService : IBookingDateValidationService
    {
        public void CheckValidTimeRange(TimeSpan startedDate, TimeSpan endedDate)
        {
            var subtractTime = endedDate - startedDate;
            if (subtractTime.TotalHours < 0)
                throw new AppException("selected wrong time range");
            
            if (endedDate.Minutes!=0 || startedDate.Minutes!=0)
                throw new AppException("selected wrong time range.please select time with out minutes");


            if (subtractTime.TotalHours < 1)
                throw new AppException("you have to select more than an hour time range");
        }
    }
}