using disasterApi.Domain.Entities;
using disasterApi.Infra.Database.Configurations;
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
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RegionConfiguration());
            builder.ApplyConfiguration(new AlertSettingConfiguration());
            builder.ApplyConfiguration(new AlertConfiguration());
        }
    }
}
