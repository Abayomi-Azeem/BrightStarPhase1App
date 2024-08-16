using BrightStarPhase1App.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrightStarPhase1App.Data.Configurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Subscribers)
                .HasForeignKey(x => x.ServiceId);
        }
    }
}
