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
        private readonly ILogger<AlertSettingController> _logger;

        public AlertSettingController(IServiceManager serviceManager, ILogger<AlertSettingController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAlertSetting([FromBody] AlertSettingForCreationDto alertSetting)
        {
            try
            {
                var alertSettings = await _serviceManager.AlertSettingService.ConfigureAlertSettingAsync(alertSetting);
                return Ok(alertSettings);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Failed to configure alert setting due to invalid input: {Message}", ex.Message); 
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while configuring alert setting for RegionID: {RegionId}, DisasterType: {DisasterType}", alertSetting.RegionId, alertSetting.DisasterType); //
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred while configuring alert settings.");
            }
        }

        [HttpGet("{regionId:guid}")]
        public async Task<IActionResult> GetAlertSettingsByRegionId(Guid regionId)
        {
            try
            {
                var alertSettings = await _serviceManager.AlertSettingService.GetAlertSettingsByRegionIdAsync(regionId);
                return Ok(alertSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving alert settings for RegionID: {RegionId}", regionId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred while retrieving alert settings.");
            }
        }
    }
}
