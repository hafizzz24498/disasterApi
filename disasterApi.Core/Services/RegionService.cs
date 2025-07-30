using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;
using disasterApi.Domain.Entities;
using disasterApi.Domain.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace disasterApi.Core.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public RegionService(IRepositoryManager Repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = Repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<RegionDto> CreateNewRegionAsync(RegionForCreationDto input)
        {
            var checkRegion = await _repository.RegionRepository.GetRegionByLatitudeAndLongtitude(input.Latitude, input.Longtitude, false);
            if (checkRegion != null)
            {
                throw new BadRequestException("Region already exists with the same latitude and longitude.");
            }

            var region = _mapper.Map<Region>(input);
            region.Id = Guid.NewGuid();
            region.CreatedAt = DateTime.UtcNow;
            region.UpdatedAt = DateTime.UtcNow;
            region.IsDeleted = false;
            
            _repository.RegionRepository.Create(region);
            await _repository.SaveAsync();

            await _cache.SetStringAsync(region.Id.ToString(), JsonSerializer.Serialize(region), CancellationToken.None);

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto> GetRegionByIdAsync(Guid id)
        {
            var cacheRegion = await _cache.GetStringAsync(id.ToString(), CancellationToken.None);

            if(cacheRegion != null)
            {
                return _mapper.Map<RegionDto>(JsonSerializer.Deserialize<Region>(cacheRegion));
            }
            var region = await _repository.RegionRepository.GetByIdAsync(id, false);

            if (region == null || region.IsDeleted)
            {
                throw new NotFoundException("Region Not Found!");
            }

            await _cache.SetStringAsync(region.Id.ToString(), JsonSerializer.Serialize(region), CancellationToken.None);
            return _mapper.Map<RegionDto>(region);
        }

        public async Task<IEnumerable<RegionDto>> GetRegionsAsync(int pageNumber, int pageSize)
        {
            var regions = await _repository.RegionRepository.GetAllAsync(false);
            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }
    }
}
