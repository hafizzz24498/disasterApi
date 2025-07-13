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

        public async Task<IEnumerable<Region>> GetAllAsync(int pageNumber, int pageSize) =>
            await FindAll(false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public Task<Region?> GetByIdAsync(Guid id) => 
            FindByCondition(region => region.Id == id, false).FirstOrDefaultAsync();
    }
}
