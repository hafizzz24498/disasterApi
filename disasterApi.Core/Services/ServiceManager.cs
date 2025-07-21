using AutoMapper;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace disasterApi.Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRegionService> _regionService;
        private readonly Lazy<IExternalApiService> _externalApiService;
        private readonly Lazy<IAlertSettingService> _alertSettingService;
        private readonly Lazy<IDisasterRiskService> _disasterRiskService;
        private readonly Lazy<IAlertService> _alertService;
        private readonly Lazy<INotificationService> _notificationService;

        public ServiceManager(IRepositoryManager repository, IMapper mapper, IHttpClientFactory httpClient, ILoggerFactory loggerFactory, IConfiguration config, IDistributedCache cache)
        {
            _regionService = new Lazy<IRegionService>(() => new RegionService(repository, mapper, cache));
            _externalApiService = new Lazy<IExternalApiService>(() => new ExternalApiService(httpClient.CreateClient(), loggerFactory.CreateLogger<ExternalApiService>(), config));
            _alertSettingService = new Lazy<IAlertSettingService>(() => new AlertSettingService(repository, mapper, loggerFactory.CreateLogger<AlertSettingService>()));
            _disasterRiskService = new Lazy<IDisasterRiskService>(() => new DisasterRiskService(repository, loggerFactory.CreateLogger<DisasterRiskService>(), _externalApiService.Value, cache));
            _notificationService = new Lazy<INotificationService>(() => new NotificationService(config));
            _alertService = new Lazy<IAlertService>(() => new AlertService(repository, mapper, _notificationService.Value, cache, _disasterRiskService.Value));
        }

        public IRegionService RegionService => _regionService.Value;

        public IExternalApiService ExternalApiService => _externalApiService.Value;

        public IAlertSettingService AlertSettingService => _alertSettingService.Value;

        public IDisasterRiskService DisasterRiskService => _disasterRiskService.Value;

        public IAlertService AlertService => _alertService.Value;

        public INotificationService NotificationService => _notificationService.Value;
    }
}
