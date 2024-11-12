using BussinessLayer.DTOs.Ncfs;
using BussinessLayer.Services.SNcfs;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Ncf
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NcfController(INcfService ncfService) : ControllerBase
    {
        private readonly INcfService _ncfService = ncfService;

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Create(CreateNcfDto createNcfDto)
        {
            Response<CreateNcfDto> ncf = await _ncfService.CreateAsync(createNcfDto);
            return ncf.Errors != null ? BadRequest(ncf) : Created("NCF creado con exito", ncf);
        }
        [Authorize]
        [HttpGet("{bussinesId}")]
        public async Task<IActionResult> Get(int bussinesId)
        {
            Response<List<NcfDto>> ncfDtos = await _ncfService.GetAllAsync(bussinesId);
            return ncfDtos.Data != null ? Ok(ncfDtos) : NoContent();
        }
        [Authorize]
        [HttpGet("{bussinesId}/{ncfType}")]
        public async Task<IActionResult> Get(int bussinesId, string ncfType)
        {
            Response<NcfDto> ncfDto = await _ncfService.GetByIdAsync(bussinesId, ncfType);
            return ncfDto != null ? Ok(ncfDto) : NoContent();
        }
        [Authorize]
        [HttpDelete("{bussinesId}/{ncfType}")]
        public async Task<IActionResult> Delete(int bussinesId, string ncfType)
        {
            Response<string> ncf = await _ncfService.DeleteAsync(bussinesId, ncfType);
            return ncf.Errors != null ? BadRequest(ncf) : NoContent();
        }

    }
}
