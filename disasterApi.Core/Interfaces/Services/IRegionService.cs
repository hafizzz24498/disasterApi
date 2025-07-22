using disasterApi.Core.Dtos;
using disasterApi.Domain.Entities;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IRegionService
    {
        Task<RegionDto> CreateNewRegionAsync(RegionForCreationDto input);
        Task<RegionDto> GetRegionByIdAsync(Guid id);
        Task<IEnumerable<RegionDto>> GetRegionsAsync(int pageNumber, int pageSize);


    }
}
