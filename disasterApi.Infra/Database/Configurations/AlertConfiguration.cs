using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace disasterApi.Infra.Database.Configurations
{
    public class AlertConfiguration : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.Region)
                   .WithMany(a => a.Alerts!)
                   .HasForeignKey(a => a.RegionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Alerts");
        }
    }
}
