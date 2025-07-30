using disasterApi.API.Extensions;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace disasterApi.API.Controllers
{
    [ApiController]
    [Route("api/alert-settings")]
    public class AlertSettingController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AlertSettingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAlertSetting([FromBody] AlertSettingForCreationDto alertSetting)
        {
            var alertSettings = await _serviceManager.AlertSettingService.ConfigureAlertSettingAsync(alertSetting);
            return Ok(alertSettings);
        }

        [HttpGet("{regionId:guid}")]
        public async Task<IActionResult> GetAlertSettingsByRegionId(Guid regionId)
        {

            var alertSettings = await _serviceManager.AlertSettingService.GetAlertSettingsByRegionIdAsync(regionId);
            return Ok(alertSettings);
            
        }
    }
}
