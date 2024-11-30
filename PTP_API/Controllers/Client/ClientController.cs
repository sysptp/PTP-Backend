using BussinessLayer.DTOs.Cliente;
using BussinessLayer.Repository.RClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientRepository clientRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto clientDto)
        {
            try
            {
                await clientRepository.CreateAsync(clientDto);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
