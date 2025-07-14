using disasterApi.Core.Interfaces.Infra.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Infra.Database.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private readonly Lazy<IRegionRepository> _regionRepository;
        private readonly Lazy<IAlertSettingRepository> _alertSettingRepository;

        public RepositoryManager(DataContext context)
        {
            _context = context;
            _regionRepository = new Lazy<IRegionRepository>(() => new RegionRepository(_context));
            _alertSettingRepository = new Lazy<IAlertSettingRepository>(() => new AlertSettingRepository(_context));
        }
        public IRegionRepository RegionRepository => _regionRepository.Value;

        public IAlertSettingRepository AlertSettingRepository => _alertSettingRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
