using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public record AlertSettingDto
    {
        public Guid RegionId { get; set; }
        public string? DisasterType { get; set; }
        public int ThresholdScore { get; set; }
    }
}
