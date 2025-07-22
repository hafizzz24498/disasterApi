using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace disasterApi.Core.Services
{
    public class AlertSettingService : IAlertSettingService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<IAlertSettingService> _logger;

        public AlertSettingService(IRepositoryManager repository, IMapper mapper, ILogger<IAlertSettingService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AlertSettingDto>> ConfigureAlertSettingAsync(AlertSettingForCreationDto dto)
        {
            _logger.LogInformation("Attempting to configure alert setting for RegionID: {RegionId}, DisasterType: {DisasterType}", dto.RegionId, dto.DisasterType);

            var region = await _repository.RegionRepository.GetByIdAsync(dto.RegionId, false);
            if (region == null)
            {
                _logger.LogWarning("Region with RegionID {RegionId} not found.", dto.RegionId);
                throw new ArgumentException($"Region with ID '{dto.RegionId}' not found.");
            }

            if (!region.DisasterTypes.Contains(dto.DisasterType, StringComparer.OrdinalIgnoreCase))
            {
                _logger.LogWarning("Disaster type '{DisasterType}' is not monitored for RegionID: {RegionId}.", dto.DisasterType, dto.RegionId);
                throw new ArgumentException($"Disaster type '{dto.DisasterType}' is not configured for monitoring in region '{dto.RegionId}'. Monitored types: {string.Join(", ", region.DisasterTypes)}");
            }

            var existingAlertSetting = await _repository.AlertSettingRepository.GetByRegionIdAndDisasterTypeAsync(region.Id, dto.DisasterType ?? "");

            if (existingAlertSetting == null)
            {

                var newAlertSetting = _mapper.Map<AlertSetting>(dto);
                newAlertSetting.RegionId = region.Id;
                newAlertSetting.CreatedAt = DateTime.UtcNow;
                newAlertSetting.UpdatedAt = DateTime.UtcNow;
                newAlertSetting.IsDeleted = false;
                _repository.AlertSettingRepository.Create(newAlertSetting);


                _logger.LogInformation("New alert setting added for RegionID: {RegionId}, DisasterType: {DisasterType}, Threshold: {ThresholdScore}", dto.RegionId, dto.DisasterType, dto.ThresholdScore);
            }
            else
            {
                // Update existing alert setting
                existingAlertSetting.ThresholdScore = dto.ThresholdScore;
                existingAlertSetting.UpdatedAt = DateTime.UtcNow;
                _repository.AlertSettingRepository.Update(existingAlertSetting);
                _logger.LogInformation("Existing alert setting updated for RegionID: {RegionId}, DisasterType: {DisasterType}, New Threshold: {ThresholdScore}", dto.RegionId, dto.DisasterType, dto.ThresholdScore);
            }

            await _repository.SaveAsync();

            return await GetAlertSettingsByRegionIdAsync(dto.RegionId);
        }

        public async Task<IEnumerable<AlertSettingDto>> GetAlertSettings()
        {
            var alertSettings = await _repository.AlertSettingRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AlertSettingDto>>(alertSettings);
        }

        public async Task<IEnumerable<AlertSettingDto>> GetAlertSettingsByRegionIdAsync(Guid regionId)
        {
            var alertSettings = await _repository.AlertSettingRepository.GetByRegionIdAsync(regionId);
            return _mapper.Map<IEnumerable<AlertSettingDto>>(alertSettings);

        }
    }
}
