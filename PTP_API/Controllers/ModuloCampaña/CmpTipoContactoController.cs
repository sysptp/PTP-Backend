using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CmpTipoContactoController : ControllerBase
{
    private readonly ICmpTipoContactoRepository _repository;

    public CmpTipoContactoController(ICmpTipoContactoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CmpTipoContacto>>> GetAll(int empresaId)
    {
        var tipoContactos = await _repository.GetAllAsync(empresaId);
        return Ok(tipoContactos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CmpTipoContacto>> GetById(int id,int empresaId)
    {
        var tipoContacto = await _repository.GetByIdAsync(id,empresaId);
        if (tipoContacto == null) return NotFound();
        return Ok(tipoContacto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CmpTipoContacto tipoContacto)
    {
        await _repository.AddAsync(tipoContacto);
        return CreatedAtAction(nameof(GetById), new { id = tipoContacto.TipoContactoId }, tipoContacto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CmpTipoContacto tipoContacto)
    {
        if (id != tipoContacto.TipoContactoId) return BadRequest();
        await _repository.UpdateAsync(tipoContacto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
