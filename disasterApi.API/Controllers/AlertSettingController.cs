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
        public async Task<IActionResult> CreateAlertSetting([FromBody] AlertSettingForCreationDto alertSetting)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for ConfigureAlertSetting: {Errors}", ModelState); //
                return StatusCode(StatusCodes.Status500InternalServerError, "rter");
            }

            try
            {
                await _serviceManager.AlertSettingService.ConfigureAlertSettingAsync(alertSetting);
                return Ok("Alert setting configured successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Failed to configure alert setting due to invalid input: {Message}", ex.Message); //
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while configuring alert setting for RegionID: {RegionId}, DisasterType: {DisasterType}", alertSetting.RegionId, alertSetting.DisasterType); //
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred while configuring alert settings.");
            }
        }
    }
}
