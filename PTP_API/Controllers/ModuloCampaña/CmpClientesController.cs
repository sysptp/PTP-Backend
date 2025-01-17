using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpClientesController(ICmpClientService clientService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(int idEmpresa)
        {
            try
            {
                Response<List<CmpClienteDto>> clientes = await clientService.GetClientsAsync(idEmpresa);

                if (!clientes.Succeeded) return BadRequest(clientes);

                return clientes.Data != null ? Ok(clientes) : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, int idEmprsa)
        {
            try
            {
                var cliente = await clientService.GetClientByIdAsync(id, idEmprsa);
                if (cliente == null) return NotFound();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CmpClientCreateDto cliente)
        {
            try
            {
                Response<CmpClientCreateDto> response = await clientService.CreateClientAsync(cliente);

                return response.Succeeded ? Created(response.Message, response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CmpClienteUpdateDto cliente)
        {
            try
            {
                if (id != cliente.ClientId) return BadRequest("Client ID mismatch.");
                await clientService.UpdateClientAsync(id, cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await clientService.DeleteClientAsync(id, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
