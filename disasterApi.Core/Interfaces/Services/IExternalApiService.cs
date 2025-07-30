using disasterApi.Core.Dtos;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task<WeatherResponseDto> GetWeatherDataAsync(double latitude, double longitude);
        Task<EarthQuakeResponseDto> GetEarthquakeDataAsync(double latitude, double longitude);
    }
}
