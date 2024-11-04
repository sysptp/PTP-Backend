using BussinessLayer.DTOs.Ncfs;
using BussinessLayer.Services.SNcfs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Ncf
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NcfController(INcfService ncfService) : ControllerBase
    {
        private readonly INcfService _ncfService = ncfService;

        [HttpPost]
        public async Task<IActionResult> Create(CreateNcfDto createNcfDto)
        {
            await _ncfService.CreateAsync(createNcfDto);
            return Ok(createNcfDto);
        }
        [HttpGet("{bussinesId}")]
        public async Task<IActionResult> Get(int bussinesId)
        {
            List<NcfDto> ncfDtos = await _ncfService.GetAllAsync(bussinesId);
            return ncfDtos.Count > 0 ? Ok(ncfDtos) : NoContent();
        }
        [HttpGet("{bussinesId}/{ncfType}")]
        public async Task<IActionResult> Get(int bussinesId,string ncfType)
        {
            NcfDto ncfDto = await _ncfService.GetByIdAsync(bussinesId, ncfType);
            return ncfDto != null ? Ok(ncfDto) : NoContent();
        }
        [HttpDelete("{bussinesId}/{ncfType}")]
        public async Task<IActionResult> Delete(int bussinesId, string ncfType)
        {
            await _ncfService.DeleteAsync(bussinesId, ncfType);
            return Ok();
        }

    }
}
