using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public record DisasterRiskReportDto
    {
        public Guid RegionId { get; set; }
        public string? DisasterType { get; set; }
        public int RiskScore { get; set; }
        public string? RiskLevel { get; set; }
        public bool AlerrtTriggered { get; set; }
    }
}
