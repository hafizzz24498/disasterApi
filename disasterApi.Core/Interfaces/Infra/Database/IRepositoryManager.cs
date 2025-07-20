using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IRepositoryManager
    {
        IRegionRepository RegionRepository { get; }
        IAlertSettingRepository AlertSettingRepository { get; }
        IAlertRepository AlertRepository { get; }
        Task SaveAsync();

    }
}
