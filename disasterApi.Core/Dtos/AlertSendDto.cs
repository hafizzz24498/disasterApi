using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public record AlertSendDto
    {
        public Guid RegionId { get; set; }
        public List<string> Methods { get; set; } = new List<string>();
        public List<string>? PhoneNumbers { get; set; } = new List<string>();
        public List<string>? Emails { get; set; } = new List<string>();
    }
}
