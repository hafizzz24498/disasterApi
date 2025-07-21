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

        public void CreateAlert(Alert alert) => Create(alert);

        public async Task<IEnumerable<Alert>> GetAlertsByRegionAsync(Guid regionId) => await FindByCondition(i => i.RegionId == regionId && i.IsDeleted.Equals(false), false).ToListAsync();

        public async Task<IEnumerable<Alert>> GetAllAlert() => await FindByCondition(i => i.IsDeleted.Equals(false), false).ToListAsync();
    }
}
