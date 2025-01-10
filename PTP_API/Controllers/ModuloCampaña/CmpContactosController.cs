using BussinessLayer.Interfaces.ModuloCampaña;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpContactosController : ControllerBase
    {
        private readonly ICmpContactosRepository _repository;

        public CmpContactosController(ICmpContactosRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CmpContactos>>> GetAll(int idEmpresa)
        {
            var contactos = await _repository.GetAllAsync(idEmpresa);
            return Ok(contactos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CmpContactos>> GetById(int id,int idEmpresa)
        {
            var contacto = await _repository.GetByIdAsync(id,idEmpresa);
            if (contacto == null) return NotFound();
            return Ok(contacto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CmpContactos contacto)
        {
            await _repository.AddAsync(contacto);
            return CreatedAtAction(nameof(GetById), new { id = contacto.ContactoId }, contacto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CmpContactos contacto)
        {
            if (id != contacto.ContactoId) return BadRequest();
            await _repository.UpdateAsync(contacto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
