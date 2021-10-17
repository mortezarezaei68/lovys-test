using System;
using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Domain;
using ScheduleManagement.Persistence.EF.Context;
using ScheduleManagement.Persistence.EF.UnitOfWork;

namespace ScheduleManagement.Persistence.EF.Repositories
{
    public class BookingDateRepository : IBookingDateRepository
    {
        private readonly ScheduleManagementDbContext _context;
        private readonly IScheduleManagementUnitOfWork _unitOfWork;

        public BookingDateRepository(ScheduleManagementDbContext context, IScheduleManagementUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public void Add(BookingDate bookingDate)
            => _context.BookingDates.Add(bookingDate);

        public void Update(BookingDate bookingDate)
            => _context.BookingDates.Update(bookingDate);

        public Task<BookingDate> GetByBookDate(DateTime bookingDate)
            => _context.BookingDates.FirstOrDefaultAsync(a => a.DateOfBooking == bookingDate);

        public IUnitOfWork UnitOfWork => _unitOfWork;
    }
}