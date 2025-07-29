using disasterApi.Domain.Entities;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IAlertRepository
    {
        Task<IEnumerable<Alert>> GetAllAlert();
        Task<IEnumerable<Alert>> GetAlertsByRegionAsync(Guid regionId);
        void BulkCreateAlert(List<Alert> alerts);
    }
}
