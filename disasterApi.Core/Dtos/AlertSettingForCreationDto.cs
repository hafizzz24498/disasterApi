using System.ComponentModel.DataAnnotations;

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
