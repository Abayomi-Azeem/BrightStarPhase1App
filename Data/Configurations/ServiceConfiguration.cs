using BrightStarPhase1App.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrightStarPhase1App.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasMany(x => x.Subscribers)
                .WithOne(x => x.Service)
                .HasForeignKey(x => x.ServiceId);

            builder.HasMany(x => x.ServiceTokens)
                .WithOne(x => x.Service)
                .HasForeignKey(x => x.ServiceId);

        }

    }
}
