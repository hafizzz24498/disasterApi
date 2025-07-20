using AutoMapper;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
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

        public ServiceManager(IRepositoryManager repository, IMapper mapper, IHttpClientFactory httpClient, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _regionService = new Lazy<IRegionService>(() => new RegionService(repository, mapper));
            _externalApiService = new Lazy<IExternalApiService>(() => new ExternalApiService(httpClient.CreateClient(), loggerFactory.CreateLogger<ExternalApiService>(), config));
            _alertSettingService = new Lazy<IAlertSettingService>(() => new AlertSettingService(repository, mapper, loggerFactory.CreateLogger<AlertSettingService>()));
            _disasterRiskService = new Lazy<IDisasterRiskService>(() => new DisasterRiskService(repository, mapper, loggerFactory.CreateLogger<DisasterRiskService>(), _externalApiService.Value));
        }

        public IRegionService RegionService => _regionService.Value;

        public IExternalApiService ExternalApiService => _externalApiService.Value;

        public IAlertSettingService AlertSettingService => _alertSettingService.Value;

        public IDisasterRiskService DisasterRiskService => _disasterRiskService.Value;
    }
}
