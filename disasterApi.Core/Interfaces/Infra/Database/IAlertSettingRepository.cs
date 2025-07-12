using disasterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IAlertSettingRepository : IBaseRepository<AlertSetting>
    {
    }
}
