using AutoMapper;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace disasterApi.Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRegionService> _regionService;
        private readonly Lazy<IExternalApiService> _externalApiService;
        private readonly Lazy<IAlertSettingService> _alertSettingService;

        public ServiceManager(IRepositoryManager repository, IMapper mapper, IHttpClientFactory httpClient, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _regionService = new Lazy<IRegionService>(() => new RegionService(repository, mapper));
            _externalApiService = new Lazy<IExternalApiService>(() => new ExternalApiService(httpClient.CreateClient(), loggerFactory.CreateLogger<ExternalApiService>(), config));
            _alertSettingService = new Lazy<IAlertSettingService>(() => new AlertSettingService(repository, mapper, loggerFactory.CreateLogger<AlertSettingService>()));
        }

        public IRegionService RegionService => _regionService.Value;

        public IExternalApiService ExternalApiService => _externalApiService.Value;

        public IAlertSettingService AlertSettingService => _alertSettingService.Value;
    }
}
