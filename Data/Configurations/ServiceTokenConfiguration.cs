using BrightStarPhase1App.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrightStarPhase1App.Data.Configurations
{
    public class ServiceTokenConfiguration : IEntityTypeConfiguration<ServiceToken>
    {
        public void Configure(EntityTypeBuilder<ServiceToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Token).HasMaxLength(100);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.ServiceTokens)
                .HasForeignKey(x => x.ServiceId);
        }
    }
}
