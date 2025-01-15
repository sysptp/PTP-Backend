using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using BussinessLayer.Interfaces.IModuloCampaña;
using BussinessLayer.Services.SModuloCampaña;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpContactosController(ICmpContactoService contactosService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idEmpresa)
        {
            try
            {
                var response = await contactosService.GetContactosAsync(idEmpresa);
                return response.Succeeded ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, int idEmpresa)
        {
            try
            {
                var response = await contactosService.GetContactoByIdAsync(id, idEmpresa);
                return response.Succeeded ? Ok(response) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CmpContactoCreateDto contacto)
        {
            try
            {
                var response = await contactosService.CreateContactoAsync(contacto);
                return response.Succeeded ? Created(response.Message, response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CmpContactoUpdateDto contacto)
        {
            try
            {
                if (id != contacto.ContactoId) return BadRequest("ID mismatch");
                var response = await contactosService.UpdateContactoAsync(id, contacto);
                return response.Succeeded ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, int idEmpresa)
        {
            try
            {
                var response = await contactosService.DeleteContactoAsync(id, idEmpresa);
                return response.Succeeded ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
