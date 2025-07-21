using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace disasterApi.API.Controllers
{
    [ApiController]
    [Route("api/alerts")]
    public class AlertController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AlertController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult>  GetDisasterRiskReportAsync()
        {
            try
            {
                var alerts = await _service.AlertService.GetAlertsAsync();
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("region/{regionId}")]
        public async Task<IActionResult> GetAlertByRegion(Guid regionId)
        {
            try
            {
                var alerts = await _service.AlertService.GetAlertsByRegionAsync(regionId);
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("send/")]
        public async Task<IActionResult> SendAlertAsync([FromBody] AlertSendDto alertSendDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _service.AlertService.SendAlertAsync(alertSendDto);
                return Ok("Alert sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
