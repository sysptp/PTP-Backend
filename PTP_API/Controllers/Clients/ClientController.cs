//using BussinessLayer.DTOs.Cliente;
//using BussinessLayer.Interfaces.IClient;
//using BussinessLayer.Wrappers;
//using DataLayer.Models.Clients;
//using Microsoft.AspNetCore.Mvc;

//namespace PTP_API.Controllers.Clients
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class ClientController(IClientService clientService) : ControllerBase
//    {
//        private readonly IClientService _clientService = clientService;

//        [HttpGet("{bussinesId}")]
//        public async Task<IActionResult> Get(int bussinesId,int pageSize, int pageCount)
//        {
//            try
//            {
//                Response<List<Client>> response = await _clientService.GetAllAsync(bussinesId,pageSize,pageCount);
//                return response.Succeeded ? Ok(response) : BadRequest(response);

//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.ToString());
//            }
//        }[HttpGet("/ById{clientId}")]
//        public async Task<IActionResult> GetById(int clientId)
//        {
//            try
//            {
//                Response<Client> response = await _clientService.GetByIdAsync(clientId);
//                return response.Succeeded ? Ok(response) : BadRequest(response);

//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.ToString());
//            }
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] CreateClientDto clientDto)
//        {
//            try
//            {
//                Response<CreateClientDto> response = await _clientService.CreateAsync(clientDto);
//                return response.Succeeded? Created("Cliente creado con exito", response) : BadRequest(response);

//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.ToString());
//            }
//        }
//    }
//}
