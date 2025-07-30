using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace disasterApi.Infra.Database.Repositories
{
    public class AlertRepository : BaseRepository<Alert>, IAlertRepository
    {
        public AlertRepository(DataContext context) : base(context)
        {
        }

        public void BulkCreateAlert(List<Alert> alerts) => BulkCreate(alerts);

        public async Task<IEnumerable<Alert>> GetAlertsByRegionAsync(Guid regionId) => await FindByCondition(i => i.RegionId == regionId, false).ToListAsync();

        public async Task<IEnumerable<Alert>> GetAllAlert() => await FindAll(false).ToListAsync();
    }
}
