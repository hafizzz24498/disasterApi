using disasterApi.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace disasterApi.API.Controllers
{
    [ApiController]
    [Route("api/disaster-risk")]
    public class DisasterRiskController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILogger<DisasterRiskController> _logger;

        public DisasterRiskController(IServiceManager serviceManager, ILogger<DisasterRiskController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewDisasterRiskAsync()
        {
            try
            {
                var riskReports = await _serviceManager.DisasterRiskService.GetDisasterRiskReportAsync();
                return Ok(riskReports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing GET /api/disaster-risks: {Message}", ex.Message); //
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred while retrieving disaster risks.");
            }
        }
    }
}
