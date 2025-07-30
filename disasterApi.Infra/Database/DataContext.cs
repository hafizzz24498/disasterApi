using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace disasterApi.Infra.Database
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }


        public DbSet<Region> Regions { get; set; }
        public DbSet<AlertSetting> AlertSettings { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>()
                .Property(r => r.DisasterTypes)
                .HasConversion(
                v => string.Join(",", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
