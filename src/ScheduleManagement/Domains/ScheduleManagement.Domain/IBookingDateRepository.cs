using System;
using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;

namespace ScheduleManagement.Domain
{
    public interface IBookingDateRepository:IRepository
    {
        void Add(BookingDate bookingDate);
        void Update(BookingDate bookingDate);
        Task<BookingDate> GetByBookDate(DateTime bookingDate);
    }
}