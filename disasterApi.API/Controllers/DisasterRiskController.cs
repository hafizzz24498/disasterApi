using disasterApi.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace disasterApi.API.Controllers
{
    [ApiController]
    [Route("api/disaster-risk")]
    public class DisasterRiskController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DisasterRiskController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateNewDisasterRiskAsync()
        {
            
            var riskReports = await _serviceManager.DisasterRiskService.GetDisasterRiskReportAsync();
            return Ok(riskReports);
            
        }
    }
}
