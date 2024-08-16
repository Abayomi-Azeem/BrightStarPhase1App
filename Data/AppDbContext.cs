using BrightStarPhase1App.Data.Configurations;
using BrightStarPhase1App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrightStarPhase1App.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<ServiceToken> ServiceTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SubscriberConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceTokenConfiguration());
        }
    }
}
