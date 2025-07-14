using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public record AlertSettingForCreationDto
    {
        [Required (ErrorMessage = "Region id is required")]
        public Guid RegionId { get; init; }
        public string? DisasterType { get; set; }
        public int ThresholdScore { get; set; }
    }
}
