using AutoMapper;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Core.Interfaces.Services;

namespace disasterApi.Core.Services
{
    public class AlertService : IAlertService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AlertService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AlertDto>> GetAlertsAsync()
        {
            var alerts = await _repository.AlertRepository.GetAllAlert();

            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }

        public async Task<IEnumerable<AlertDto>> GetAlertsByRegionAsync(Guid regionId)
        {
            var alerts = await _repository.AlertRepository.GetAlertsByRegionAsync(regionId);
            return _mapper.Map<IEnumerable<AlertDto>>(alerts);
        }
    }
}
