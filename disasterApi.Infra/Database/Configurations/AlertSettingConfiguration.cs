using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Infra.Database.Configurations
{
    public class AlertSettingConfiguration : IEntityTypeConfiguration<AlertSetting>
    {
        public void Configure(EntityTypeBuilder<AlertSetting> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(a => a.Region)
                   .WithMany()
                   .HasForeignKey(a => a.RegionId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("AlertSettings");
        }
    }
}
