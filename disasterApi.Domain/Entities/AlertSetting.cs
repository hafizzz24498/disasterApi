using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Domain.Entities
{
    public class AlertSetting : BaseEntity, IBaseEntity
    {
        public Guid RegionId { get; set; }
       public string? DisasterType { get; set; }
        public double ThresholdScore { get; set; }

        public Region? Region { get; set; }
    }
}
