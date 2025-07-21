using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Core.Services.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace disasterApi.Core.Services
{
    public class DisasterRiskService : IDisasterRiskService
    {

        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DisasterRiskService> _logger;
        private readonly IExternalApiService _externalDataService;
        private readonly RiskCalculateService _riskCalculateService;

        public DisasterRiskService(IRepositoryManager repository, IMapper mapper, ILogger<DisasterRiskService> logger, IExternalApiService externalApiService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _externalDataService = externalApiService;
            _riskCalculateService = new RiskCalculateService();
        }
        public async Task<List<DisasterRiskReportDto>> GetDisasterRiskReportAsync()
        {
            _logger.LogInformation("Fetching disaster risk report...");
            var riskReports = new List<DisasterRiskReportDto>();
            var regions = await _repository.RegionRepository.GetAllAsync();
            if (regions == null || !regions.Any())
            {
                _logger.LogWarning("No regions found for disaster risk report.");
                return riskReports;
            }


            foreach (var region in regions)
            {
                _logger.LogDebug("Assessing risks for Region: {RegionID} ({Latitude}, {Longitude})", region.Id, region.Latitude, region.Longitude);
                var alertSettings = await _repository.AlertSettingRepository.GetByRegionIdAsync(region.Id);

                foreach (var disasterType in region.DisasterTypes)
                {
                    int riskScore = 0;
                    WeatherResponseDto? weatherData = null;
                    EarthQuakeResponseDto? seismicData = null;
                    bool dataFetched = false;

                    try
                    {
                        switch (disasterType.ToLower())
                        {
                            case "flood":
                                weatherData = await _externalDataService.GetWeatherDataAsync(region.Latitude, region.Longitude);
                                riskScore = RiskCalculateService.CalculateFloodRisk(weatherData.Current?.Rain?.OneHour ?? 0);
                                dataFetched = true;
                                break;
                            case "earthquake":
                                seismicData = await _externalDataService.GetEarthquakeDataAsync(region.Latitude, region.Longitude);
                                riskScore = RiskCalculateService.CalculateEarthquakeRisk(seismicData?.Features?.Max(i => i.Properties?.Mag ?? 0) ?? 0);
                                dataFetched = true;
                                break;
                            case "wildfire":
                                weatherData = await _externalDataService.GetWeatherDataAsync(region.Latitude, region.Longitude);
                                riskScore = RiskCalculateService.CalculateWildfireRisk(weatherData?.Current?.Temp ?? 0, weatherData?.Current?.Humidity ?? 0);
                                dataFetched = true;
                                break;
                            default:
                                _logger.LogWarning("Unsupported disaster type '{DisasterType}' for region {RegionID}.", disasterType, region.Id);
                                break;
                        }

                        if (!dataFetched)
                        {
                            _logger.LogWarning("No data fetched for unsupported disaster type '{DisasterType}' in region {RegionID}. Defaulting to low risk.", disasterType, region.Id);
                            riskScore = 0;
                        }
                    }
                    catch (Exception ex) 
                    {
                        _logger.LogError(ex, "An unexpected error occurred while fetching data for {DisasterType} in {RegionID}. Defaulting to low risk.", disasterType, region.Id); 
                        riskScore = 0;
                    }

                    var currentReport = new DisasterRiskReportDto
                    {
                        RegionId = region.Id,
                        DisasterType = disasterType,
                        RiskScore = riskScore,
                        RiskLevel = RiskCalculateService.GetRiskLevel(riskScore),
                        AlerrtTriggered = false
                    };

                    
                    var relevantAlertSetting = alertSettings.FirstOrDefault(s => s.DisasterType.Equals(disasterType, StringComparison.OrdinalIgnoreCase));
                    if (relevantAlertSetting != null && riskScore >= relevantAlertSetting.ThresholdScore)
                    {
                        currentReport.AlerrtTriggered = true;
                        _logger.LogWarning("Alert triggered for {DisasterType} in {RegionID}! Score: {RiskScore}, Threshold: {Threshold}", disasterType, region.Id, riskScore, relevantAlertSetting.ThresholdScore);
                    }

                    riskReports.Add(currentReport);
                }
            }
            _logger.LogInformation("Disaster risk assessment completed. Generated {Count} reports.", riskReports.Count);
            return riskReports;
        }
    }
}
