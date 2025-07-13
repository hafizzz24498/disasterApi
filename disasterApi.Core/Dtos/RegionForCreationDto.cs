using System.ComponentModel.DataAnnotations;

namespace disasterApi.Core.Dtos
{
    public record RegionForCreationDto
    {
        [Required(ErrorMessage = "Latitude is required.")]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; init; }

        [Required(ErrorMessage = "Longitude is required.")]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; init; }

        [Required(ErrorMessage = "At least one disaster type is required.")]
        [MinLength(1, ErrorMessage = "At least one disaster type must be specified.")]
        public List<string> DisasterTypes { get; set; } = new List<string>();
    }
}
