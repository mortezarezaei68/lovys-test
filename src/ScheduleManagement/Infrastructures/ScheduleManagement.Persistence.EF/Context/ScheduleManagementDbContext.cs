using Framework.Context;
using Microsoft.EntityFrameworkCore;
using ScheduleManagement.Domain;
using ScheduleManagement.Persistence.EF.DomainConfigurations;

namespace ScheduleManagement.Persistence.EF.Context
{
    public class ScheduleManagementDbContext:CoreDbContext
    {
        public ScheduleManagementDbContext(DbContextOptions<ScheduleManagementDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDateEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BookingDate> BookingDates { get; set; }
        public DbSet<BookingTime> BookingTimes { get; set; }
        public DbSet<BookingDateBookingTimeView> BookingDateBookingTimeViews { get; set; }
    }
}