using disasterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IAlertSettingRepository
    {
        Task<AlertSetting?> GetByRegionIdAndDisasterTypeAsync(Guid regionId, string disasterType);
        void Create(AlertSetting alertSetting);
        void Update(AlertSetting alertSetting);
        Task<IEnumerable<AlertSetting>> GetAllAsync();
        Task<IEnumerable<AlertSetting>> GetByRegionIdAsync(Guid regionId);
    }
}
