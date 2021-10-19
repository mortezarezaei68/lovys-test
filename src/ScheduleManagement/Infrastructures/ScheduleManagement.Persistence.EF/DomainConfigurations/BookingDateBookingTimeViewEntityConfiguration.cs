using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManagement.Domain;

namespace ScheduleManagement.Persistence.EF.DomainConfigurations
{
    public class BookingDateBookingTimeViewEntityConfiguration : IEntityTypeConfiguration<BookingDateBookingTimeView>
    {
        public void Configure(EntityTypeBuilder<BookingDateBookingTimeView> builder)
        {
            builder.HasNoKey();
            builder.ToView("vwBookingDateAndBookingTime");
        }
    }
}