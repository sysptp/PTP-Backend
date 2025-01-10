using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.Interfaces.IModuloCampaña;
using BussinessLayer.Interfaces.ModuloCampaña;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Http;
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
            Response<List<CmpClienteDto>> clientes = await clientService.GetClientsAsync(idEmpresa);

            if (!clientes.Succeeded) return BadRequest(clientes);

            return clientes.Data != null ? Ok(clientes) : NoContent();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await clientService.GetClientsAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CmpClientCreateDto cliente)
        {
            Response<CmpClientCreateDto> response = await clientService.CreateClientAsync(cliente);

            return response.Succeeded ? Created(response.Message, response) : BadRequest(response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CmpClienteUpdateDto cliente)
        {
            if (id != cliente.ClienteId) return BadRequest();
            await clientService.UpdateClientAsync(id, cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await clientService.DeleteClientAsync(id, id);
            return NoContent();
        }
    }
}
