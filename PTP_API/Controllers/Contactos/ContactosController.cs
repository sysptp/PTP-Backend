using BussinessLayer.DTOs.Contactos.ClienteContacto;
using BussinessLayer.DTOs.Contactos.TypeContact;
using BussinessLayer.Services.SCliente;
using BussinessLayer.Services.SContactos;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.Contactos
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactosController(IContactService contactService,IClientContactRepository clientContactRepository) : ControllerBase
    {
        [HttpGet("type-contact/{bussinesId}")]
        public async Task<IActionResult> GetTypeContacts(int bussinesId)
        {
            try
            {
                Response<List<TypeContactDto>> response = await contactService.GetTypeContactAsync(bussinesId);
                return response.Succeeded ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        [HttpPost("type-contact")]
        public async Task<IActionResult> Create(TypeContactRequest contactRequest)
        {
            try
            {
                Response<TypeContactRequest> response = await contactService.CreateContactAsync(contactRequest);
                return response.Succeeded ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(ClientContactDto dto)
        {
            await clientContactRepository.CreateAsync(dto);
            return Ok(dto);
        }
    }
}
