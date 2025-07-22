using disasterApi.Domain.Entities;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IRegionRepository
    {
        void Create(Region region);
        Task<Region?> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Region>> GetAllAsync(bool trackChanges);
    }
}
