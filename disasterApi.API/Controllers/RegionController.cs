using disasterApi.API.Extensions;
using disasterApi.Core.Dtos;
using disasterApi.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace disasterApi.API.Controllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public RegionController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<RegionDto>> CreateNewRegionAsync([FromBody] RegionForCreationDto input)
        {
          
            return Ok(await _service.RegionService.CreateNewRegionAsync(input));
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegionByIdAsync(Guid id)
        {
           
            return Ok(await _service.RegionService.GetRegionByIdAsync(id));
           
        }

        [HttpGet]
        public async Task<ActionResult<RegionDto>> GetRegionsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            
            return Ok(await _service.RegionService.GetRegionsAsync(pageNumber, pageSize));
            
        }
    }
}
