using disasterApi.Core.Dtos;
using disasterApi.Domain.Entities;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IRegionService
    {
        Task<RegionDto> CreateNewRegionAsync(RegionForCreationDto input);
        Task<RegionDto> UpdateRegionAsync(Guid id, RegionForCreationDto input);
        Task<RegionDto> GetRegionByIdAsync(Guid id);
        Task<RegionDto> DeleteRegionAsync(Guid id);
        Task<PaginationResponse<RegionDto>> GetRegionsAsync(int pageNumber, int pageSize);


    }
}
