using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace disasterApi.Infra.Database.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Region>> GetAllAsync(bool trackChanges) =>
            await FindByCondition(i => i.IsDeleted.Equals(false), trackChanges).ToListAsync();

        public Task<Region?> GetByIdAsync(Guid id, bool trackChanges) => 
            FindByCondition(i => i.Id == id && i.IsDeleted.Equals(false), false).FirstOrDefaultAsync();
    }
}
