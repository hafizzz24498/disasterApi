using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace disasterApi.Core.Services
{
    public class AlertService : IAlertService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AlertService(IRepositoryManager repository, IMapper mapper, INotificationService notificationService)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationService = notificationService;
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
            var alertSetting = await _repository.AlertSettingRepository.GetByRegionIdAsync(alertSendDto.RegionId);
            //if (alertSendDto.Methods.Contains("Message"))
            //{
            //    if (alertSendDto.PhoneNumbers == null || alertSendDto.PhoneNumbers.Count == 0)
            //    {
            //        throw new ArgumentException("Phone number list cannot be null or empty when sending message alerts.");
            //    }

            //    foreach (var phoneNumber in alertSendDto.PhoneNumbers)
            //    {
            //        await _notificationService.SendMessageAsync(alertSendDto.Message, phoneNumber);
            //    }
            //}
            //if (alertSendDto.Methods.Contains("Email"))
            //{
            //    if (alertSendDto.Emails == null || alertSendDto.Emails.Count == 0)
            //    {
            //        throw new ArgumentException("Email list cannot be null or empty when sending email alerts.");
            //    }
            //    await _notificationService.SendEmailAsync("alert", alertSendDto.Message, alertSendDto.Emails);
            //}

            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                RegionId = alertSendDto.RegionId,
                DisasterType = alertSendDto.DisasterType,
                AlertMessage = alertSendDto.Message,
                Timestamp = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,

            };

            _repository.AlertRepository.CreateAlert(alert);
            try
            {
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new InvalidOperationException($"Failed to save alert. {ex.Message}");

            }
        }
    }
}
