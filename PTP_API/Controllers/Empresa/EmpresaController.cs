using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.IEmpresa;

namespace PTP_API.Controllers.Empresa
{

 
    namespace PTP_API.Controllers.Empresa
    {
        [ApiController]
        [Route("api/v{version:apiVersion}/[controller]")]
        [ApiVersion("1.0")]
        [SwaggerTag("Gestión de Empresas")]
        public class SC_EMP001Controller : ControllerBase
        {
            private readonly ISC_EMP001service _scEmp001Service;

            public SC_EMP001Controller(ISC_EMP001service scEmp001Service)
            {
                _scEmp001Service = scEmp001Service;
            }

            // GET: api/v1/SC_EMP001
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [SwaggerOperation(Summary = "Obtener empresas", Description = "Obtiene una lista de todas las empresas o una empresa específica si se proporciona un ID.")]
            public async Task<ActionResult<IEnumerable<SC_EMP001Dto>>> Get([FromQuery] int? id)
            {
                if (id.HasValue)
                {
                    var empresa = await _scEmp001Service.GetByIdSaveDto(id.Value);
                    if (empresa == null)
                    {
                        return NotFound("Empresa no encontrada.");
                    }
                    return Ok(empresa);
                }
                else
                {
                    var empresas = await _scEmp001Service.GetAllDto();
                    if (empresas == null || empresas.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(empresas);
                }
            }

            // POST: api/v1/SC_EMP001
            [HttpPost]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [SwaggerOperation(Summary = "Crear una nueva empresa", Description = "Crea una nueva empresa en el sistema.")]
            public async Task<ActionResult> Add([FromBody] SaveSC_EMP001Dto saveDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var empresa = await _scEmp001Service.Add(saveDto);

                return CreatedAtAction(nameof(Get), new { id = empresa.CODIGO_EMP }, empresa);
            }

            // PUT: api/v1/SC_EMP001/{id}
            [HttpPut("{id}")]
            [Consumes(MediaTypeNames.Application.Json)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [SwaggerOperation(Summary = "Actualizar una empresa", Description = "Actualiza la información de una empresa existente.")]
            public async Task<ActionResult> Update(int id, [FromBody] SaveSC_EMP001Dto saveDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingEmpresa = await _scEmp001Service.GetByIdSaveDto(id);
                if (existingEmpresa == null)
                    return NotFound("Empresa no encontrada.");

                await _scEmp001Service.Update(saveDto, id);
                return NoContent();
            }

            // DELETE: api/v1/SC_EMP001/{id}
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [SwaggerOperation(Summary = "Eliminar una empresa", Description = "Elimina una empresa de manera lógica.")]
            public async Task<ActionResult> Delete(int id)
            {
                var empresa = await _scEmp001Service.GetByIdSaveDto(id);
                if (empresa == null)
                    return NotFound("Empresa no encontrada.");

                await _scEmp001Service.Delete(id);
                return NoContent();
            }
        }
    }

}
