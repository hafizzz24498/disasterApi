using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace disasterApi.Core.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;
        private readonly IConfiguration _config;
        public ExternalApiService(
            HttpClient httpClient,
            ILogger<ExternalApiService> logger,
            IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
        }

        public Task<EarthquakeData> GetEarthquakeDataAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude)
        {
            try
            {
                var apiUrl = _config["WeatherApi:BaseUrl"];
                var apiKey = _config["WeatherApi:ApiKey"];
                var response = await _httpClient.GetAsync($"{apiUrl}?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Weather data fetched successfully for coordinates ({Latitude}, {Longitude})", latitude, longitude);

                return JsonConvert.DeserializeObject<WeatherData>(content) ?? new WeatherData();
            } catch(HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching weather data for coordinates ({Latitude}, {Longitude})", latitude, longitude);
                throw new Exception("Failed to fetch weather data. Please try again later.", ex);
            }
        }
    }
}
