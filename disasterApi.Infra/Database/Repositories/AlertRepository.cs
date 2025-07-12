using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Infra.Database.Repositories
{
    public class AlertRepository : BaseRepository<Alert>, IAlertRepository
    {
        public AlertRepository(DataContext context) : base(context)
        {
        }
    }
}
