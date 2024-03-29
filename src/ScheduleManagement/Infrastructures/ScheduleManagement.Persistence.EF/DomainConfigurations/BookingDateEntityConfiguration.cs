using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManagement.Domain;

namespace ScheduleManagement.Persistence.EF.DomainConfigurations
{
    public class BookingDateEntityConfiguration:IEntityTypeConfiguration<BookingDate>
    {
        public void Configure(EntityTypeBuilder<BookingDate> builder)
        {
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateOfBooking).HasColumnType("date");
            builder.HasMany(a => a.BookingTimes);
        }
    }
}