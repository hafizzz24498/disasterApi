using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Core.Services.Extensions;
using disasterApi.Domain.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace disasterApi.Core.Services
{
    public class DisasterRiskService : IDisasterRiskService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILogger<DisasterRiskService> _logger;
        private readonly IExternalApiService _externalDataService;
        private readonly IDistributedCache _cache;

        public DisasterRiskService(IRepositoryManager repository, ILogger<DisasterRiskService> logger, IExternalApiService externalApiService, IDistributedCache cache)
        {
            _repository = repository;
            _logger = logger;
            _externalDataService = externalApiService;
            _cache = cache;
        }
        public async Task<List<DisasterRiskReportDto>> GetDisasterRiskReportAsync()
        {
            var riskReports = new List<DisasterRiskReportDto>();
            var regions = await _repository.RegionRepository.GetAllAsync(false);
            if (regions == null || !regions.Any())
            {
                _logger.LogWarning("No regions found for disaster risk report.");
                throw new NotFoundException("Region Not Found!");
            }


            foreach (var region in regions)
            {
                var alertSettings = await _repository.AlertSettingRepository.GetByRegionIdAsync(region.Id);

                foreach (var disasterType in region.DisasterTypes)
                {
                    int riskScore = 0;
                    WeatherResponseDto? weatherData = null;
                    EarthQuakeResponseDto? seismicData = null;

                    switch (disasterType.ToLower())
                    {
                        case "flood":
                            weatherData = await _externalDataService.GetWeatherDataAsync(region.Latitude, region.Longtitude);
                            riskScore = RiskCalculateService.CalculateFloodRisk(weatherData.Current?.Rain?.OneHour ?? 0);
                            break;
                        case "earthquake":
                            seismicData = await _externalDataService.GetEarthquakeDataAsync(region.Latitude, region.Longtitude);
                            riskScore = RiskCalculateService.CalculateEarthquakeRisk(seismicData?.Features?.Any() == true
                                ? seismicData.Features.Max(i => i.Properties?.Mag ?? 0)
                                : 0);
                            break;
                        case "wildfire":
                            weatherData = await _externalDataService.GetWeatherDataAsync(region.Latitude, region.Longtitude);
                            riskScore = RiskCalculateService.CalculateWildfireRisk(weatherData?.Current?.Temp ?? 0, weatherData?.Current?.Humidity ?? 0);
                            break;
                        default:
                            _logger.LogWarning("Unsupported disaster type '{DisasterType}' for region {RegionID}.", disasterType, region.Id);
                            break;
                    }

                    var riskLevel = RiskCalculateService.GetRiskLevel(riskScore);
                    var alertSetting = alertSettings.FirstOrDefault(s => s.DisasterType.Equals(disasterType, StringComparison.OrdinalIgnoreCase));
                    var alertTriggered = alertSetting != null && riskScore >= alertSetting.ThresholdScore;


                    var currentReport = new DisasterRiskReportDto
                    {
                        RegionId = region.Id,
                        DisasterType = disasterType,
                        RiskScore = riskScore,
                        RiskLevel = riskLevel,
                        AlerrtTriggered = alertTriggered
                    };

                    riskReports.Add(currentReport);
                }
            }
            _logger.LogInformation("Disaster risk assessment completed. Generated {Count} reports.", riskReports.Count);

            var options = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync("DisasterRiskData", JsonSerializer.Serialize(riskReports), options, CancellationToken.None);

            return riskReports;
        }
    }
}
