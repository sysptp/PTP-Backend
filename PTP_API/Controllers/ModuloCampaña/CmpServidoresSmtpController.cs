using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpServidoresSmtpController(ICmpServidoresSmtpService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Response<List<CmpServidoresSmtpDto>> result = await service.GetAllAsync();

                return result.Data != null && result.Data.Count > 0 ? Ok(result) : NoContent();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Response<CmpServidoresSmtpDto> result = await service.GetByIdAsync(id);

                return result.Data != null ? Ok(result) : NoContent();

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CmpServidoresSmtpCreateDto dto)
        {
            try
            {
                Response<CmpServidoresSmtpCreateDto> result = await service.CreateAsync(dto);
                return result.Succeeded ? Created("Recurso creado", result) : BadRequest(result);


            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CmpServidoresSmtpUpdateDto dto)
        {
            try
            {
                Response<CmpServidoresSmtpUpdateDto> result = await service.UpdateAsync(dto);
                return result.Succeeded ? NoContent() : BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
