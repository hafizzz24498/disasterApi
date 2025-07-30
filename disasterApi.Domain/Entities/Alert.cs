using System.ComponentModel.DataAnnotations;

namespace disasterApi.Domain.Entities
{
    public class Alert
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public string DisasterType { get; set; } = string.Empty;
        [Required]
        public string RiskLevel { get; set; } = string.Empty;
        [Required]
        public string AlertMessage { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        public Region? Region { get; set; }
    }
}
