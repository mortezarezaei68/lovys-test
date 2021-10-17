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
            builder.OwnsMany(p => p.BookingTimes, bookingTime =>
            {
                bookingTime.Property(p => p.SubjectId);
                bookingTime.Property(p => p.EndedBookingTime);
                bookingTime.Property(p => p.StartedBookingTime);
                bookingTime.ToTable("BookingTimes");
                bookingTime.Property<int>("Id");
                bookingTime.HasKey("Id");
                bookingTime.WithOwner().HasForeignKey("BookingTimeId");
            });
        }
    }
}