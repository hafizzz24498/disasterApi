using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IServiceManager
    {
        IRegionService RegionService { get; }
        IExternalApiService ExternalApiService { get; }
        IAlertSettingService AlertSettingService { get; }
        IDisasterRiskService DisasterRiskService { get; }
    }
}
