using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Dtos
{
    public class WeatherData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RainfallMm { get; set; }
        public double TemperatureCelsius { get; set; }
        public double HumidityPercentage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
