using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Domain.Entities;
using disasterApi.Domain.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace disasterApi.Core.Services
{
    public class AlertService : IAlertService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IDistributedCache _cache;
        private readonly IDisasterRiskService _disasterRiskService;

        public AlertService(IRepositoryManager repository, IMapper mapper, INotificationService notificationService, IDistributedCache cache, IDisasterRiskService disasterRiskService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
            _cache = cache;
            _disasterRiskService = disasterRiskService;
        }
        public async Task<IEnumerable<AlertDto>> GetAlertsAsync()
        {
 
            var alerts = await _repository.AlertRepository.GetAllAlert();

            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }

        public async Task<IEnumerable<AlertDto>> GetAlertsByRegionAsync(Guid regionId)
        {
            var alerts = await _repository.AlertRepository.GetAlertsByRegionAsync(regionId);
            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }

        public async Task SendAlertAsync(AlertSendDto alertSendDto)
        {

            var cacheData = await _cache.GetStringAsync("DisasterRiskData");
            var risks = !string.IsNullOrEmpty(cacheData)
                ? JsonSerializer.Deserialize<List<DisasterRiskReportDto>>(cacheData) ?? new()
                : await _disasterRiskService.GetDisasterRiskReportAsync();

            var getRiskReportByRegion = risks.Where(i => i.RegionId == alertSendDto.RegionId && (i.RiskLevel == "High" || i.AlerrtTriggered == true)).ToList();
            if(getRiskReportByRegion.Count == 0)
            {
                throw new BadRequestException("No risk report for this region to alert");
            }

            List<Alert> alerts= new List<Alert>();
            foreach(var disasterRisk in getRiskReportByRegion)
            {

                var message = GenerateAlertMessage(disasterRisk.DisasterType ?? "");
                if (alertSendDto.Methods.Contains("Message"))
                {
                    if (alertSendDto.PhoneNumbers == null || alertSendDto.PhoneNumbers.Count == 0)
                    {
                        throw new BadRequestException("Phone number list cannot be null or empty when sending message alerts.");
                    }

                    foreach (var phoneNumber in alertSendDto.PhoneNumbers)
                    {
                        await _notificationService.SendMessageAsync(message,phoneNumber);
                    }
                }
                if (alertSendDto.Methods.Contains("Email"))
                {
                    if (alertSendDto.Emails == null || alertSendDto.Emails.Count == 0)
                    {
                        throw new BadRequestException("Email list cannot be null or empty when sending email alerts.");
                    }
                    await _notificationService.SendEmailAsync($"Alert-{disasterRisk.DisasterType}", message, alertSendDto.Emails);
                }

                var alert = new Alert
                {
                    Id = Guid.NewGuid(),
                    RegionId = alertSendDto.RegionId,
                    DisasterType = disasterRisk.DisasterType ?? "",
                    AlertMessage = message,
                    Timestamp = DateTime.UtcNow,
                };
                alerts.Add(alert);
            }

            _repository.AlertRepository.BulkCreateAlert(alerts);
            await _repository.SaveAsync();
        }

        private static string GenerateAlertMessage(string disasterType)
        {
            return disasterType.ToLower() switch
            {
                "flood" => $"[FLOOD WARNING] Severe flooding risk detected in . Please evacuate if necessary and stay tuned to local news.",
                "earthquake" => $" [EARTHQUAKE ALERT] Possible seismic activity near . Stay away from buildings and take safety measures.",
                "wildfire" => $" [WILDFIRE ALERT] Wildfire threat detected in . Prepare for possible evacuation.",
                _ => $"⚠️ [DISASTER ALERT] A high-risk event ({disasterType}) has been detected in . Stay alert and follow safety instructions."
            };
        }

    }
}
