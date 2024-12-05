using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Interfaces.IClient;
using BussinessLayer.Wrappers;
using DataLayer.Models.Clients;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Clients
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpGet("{bussinesId}")]
        public async Task<IActionResult> Get(int bussinesId)
        {
            try
            {
                Response<List<Client>> response = await _clientService.GetAllAsync(bussinesId);
                return response.Succeeded ? Ok(response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto clientDto)
        {
            try
            {
                Response<CreateClientDto> response = await _clientService.CreateAsync(clientDto);
                return response.Succeeded? Created("Cliente creado con exito", response) : BadRequest(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
