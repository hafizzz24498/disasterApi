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
        public async Task<ActionResult<RegionDto>> CreateNewRegionAsync([FromBody] RegionForCreationDto input)
        {
            try
            {
                return Ok(await _service.RegionService.CreateNewRegionAsync(input));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RegionDto>> UpdateRegionAsync(Guid id, [FromBody] RegionForCreationDto input)
        {
            try
            {
                return Ok(await _service.RegionService.UpdateRegionAsync(id, input));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RegionDto>> DeleteRegionAsync(Guid id)
        {
            try
            {
                return Ok(await _service.RegionService.DeleteRegionAsync(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionDto>> GetRegionByIdAsync(Guid id)
        {
            try
            {
                return Ok(await _service.RegionService.GetRegionByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<RegionDto>> GetRegionsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                return Ok(await _service.RegionService.GetRegionsAsync(pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
