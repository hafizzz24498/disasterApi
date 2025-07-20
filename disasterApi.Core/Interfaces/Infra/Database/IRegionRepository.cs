using disasterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IRegionRepository
    {
        void Create(Region region);
        void Delete(Region region);
        void Update(Region region);
        Task<Region?> GetByIdAsync(Guid id);
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
