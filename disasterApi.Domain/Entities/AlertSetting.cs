using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Domain.Entities
{
    public class AlertSetting 
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public string DisasterType { get; set; } =string.Empty;
        [Required]
        public double ThresholdScore { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public Region? Region { get; set; }

    }
}
