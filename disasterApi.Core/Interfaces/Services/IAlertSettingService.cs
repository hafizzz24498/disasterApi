using disasterApi.Core.Dtos;

namespace disasterApi.Core.Interfaces.Services
{
    public interface IAlertSettingService
    {
        Task<IEnumerable<AlertSettingDto>> ConfigureAlertSettingAsync(AlertSettingForCreationDto alertSettingForCreationDto);
        Task<IEnumerable<AlertSettingDto>> GetAlertSettings();
        Task<IEnumerable<AlertSettingDto>> GetAlertSettingsByRegionIdAsync(Guid regionId);
    }
}
