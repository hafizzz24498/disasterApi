using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Infra.Database.Repositories
{
    public class AlertSettingRepository : BaseRepository<AlertSetting>, IAlertSettingRepository
    {
        public AlertSettingRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AlertSetting>> GetAllAsync() => await FindByCondition(i => i.IsDeleted.Equals(false), false).ToListAsync();

        public async Task<AlertSetting?> GetByRegionIdAndDisasterTypeAsync(Guid regionId, string disasterType)
        {
            return await FindByCondition(i => i.RegionId == regionId && i.DisasterType == disasterType && i.IsDeleted.Equals(false), false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AlertSetting>> GetByRegionIdAsync(Guid regionId)
        {
            var alertSetting =  await FindByCondition(i => i.RegionId == regionId && i.IsDeleted.Equals(false), false).ToListAsync();
            return alertSetting;
        }
    }
}
