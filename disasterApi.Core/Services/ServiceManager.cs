using AutoMapper;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IRegionService> _regionService;

        public ServiceManager(IRepositoryManager repository, IMapper mapper)
        {
            _regionService = new Lazy<IRegionService>(() => new RegionService(repository, mapper));
        }

        public IRegionService RegionService => _regionService.Value;
    }
}
