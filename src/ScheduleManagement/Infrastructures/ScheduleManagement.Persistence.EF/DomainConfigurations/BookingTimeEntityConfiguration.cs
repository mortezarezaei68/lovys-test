using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleManagement.Domain;

namespace ScheduleManagement.Persistence.EF.DomainConfigurations
{
    public class BookingTimeEntityConfiguration:IEntityTypeConfiguration<BookingTime>
    {
        public void Configure(EntityTypeBuilder<BookingTime> builder)
        {
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.HasKey(a => a.Id);
        }
    }
}