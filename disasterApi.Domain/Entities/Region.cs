using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Domain.Entities
{
    public class Region : BaseEntity, IBaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> DisasterTypes { get; set; } = new();

        public ICollection<AlertSetting>? AlertSettings { get; set; }
        public ICollection<Alert>? Alerts { get; set; }
    }
}
