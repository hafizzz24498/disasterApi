using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Domain.Entities
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public List<string> DisasterTypes { get; set; } = new List<string>();
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public ICollection<AlertSetting>? AlertSettings { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
    }
}
