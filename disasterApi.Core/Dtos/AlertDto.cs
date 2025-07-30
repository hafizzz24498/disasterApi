using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public record AlertDto
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public string? DisasterType { get; set; } 
        public string? RiskLevel { get; set; } 
        public string? AlertMessage { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
