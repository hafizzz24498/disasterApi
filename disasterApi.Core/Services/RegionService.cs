using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Domain.Entities;

namespace disasterApi.Core.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public RegionService(IRepositoryManager Repository, IMapper mapper)
        {
            _repository = Repository;
            _mapper = mapper;
        }

        public async Task<RegionDto> CreateNewRegionAsync(RegionForCreationDto input)
        {
            var region = _mapper.Map<Region>(input);
            region.Id = Guid.NewGuid();
            region.CreatedAt = DateTime.UtcNow;
            region.UpdatedAt = DateTime.UtcNow;
            region.IsDeleted = false;
            _repository.RegionRepository.Create(region);
            await _repository.SaveAsync();

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto> DeleteRegionAsync(Guid id)
        {
            var region = await _repository.RegionRepository.GetByIdAsync(id);

            if (region == null || region.IsDeleted== true)
            {
                throw new ArgumentException("Region not found");
            }

            region.IsDeleted = true;
            region.UpdatedAt = DateTime.UtcNow;
            _repository.RegionRepository.Update(region);
            await _repository.SaveAsync();

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto> GetRegionByIdAsync(Guid id)
        {
            var region = await _repository.RegionRepository.GetByIdAsync(id);

            if (region == null || region.IsDeleted)
            {
                throw new ArgumentException("Region not found");
            }

            return _mapper.Map<RegionDto>(region);
        }

        public Task<PaginationResponse<RegionDto>> GetRegionsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException("Paging not implemented yet");
        }

        public async Task<RegionDto> UpdateRegionAsync(Guid id, RegionForCreationDto input)
        {
            var region = await _repository.RegionRepository.GetByIdAsync(id);
            if (region == null || region.IsDeleted == true)
            {
                throw new ArgumentException("Region not found");
            }
            
            _mapper.Map(input, region);
            region.UpdatedAt = DateTime.UtcNow;
            _repository.RegionRepository.Update(region);
            await _repository.SaveAsync();

            return _mapper.Map<RegionDto>(region);
        }
    }
}
