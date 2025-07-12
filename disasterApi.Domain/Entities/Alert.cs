using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Domain.Entities
{
    public class Alert : BaseEntity, IBaseEntity
    {
        public Guid RegionId { get; set; }
        public string? DisasterType { get; set; }
        public string? RiskLevel { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime AlertDate { get; set; } = DateTime.UtcNow;

        public Region? Region { get; set; }
    }
}
