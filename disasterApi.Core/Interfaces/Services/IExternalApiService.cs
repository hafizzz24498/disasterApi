using disasterApi.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude);
        Task<EarthquakeData> GetEarthquakeDataAsync(double latitude, double longitude);
    }
}
