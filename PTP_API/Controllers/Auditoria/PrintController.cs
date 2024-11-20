using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.Auditoria;

namespace PTP_API.Controllers.Auditoria
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Print")]
    [Authorize]
    public class PrintController : ControllerBase
    {
        private readonly IAlePrintService _AlePrintService;
        private readonly IValidator<AlePrintRequest> _validator;

        public PrintController(IAlePrintService AlePrintService, IValidator<AlePrintRequest> validator)
        {
            _AlePrintService = AlePrintService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AlePrintReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Print Auditoria", Description = "Obtiene una lista de todas las Print Auditoria o una Print Auditoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var AlePrint = await _AlePrintService.GetByIdResponse(id);
                    if (AlePrint == null)
                    {
                        return NotFound(Response<AlePrintReponse>.NotFound("Print Auditoria no encontrada."));
                    }
                    return Ok(Response<List<AlePrintReponse>>.Success(new List<AlePrintReponse> { AlePrint }, "Print Auditoria encontrada."));
                }
                else
                {
                    var AlePrints = await _AlePrintService.GetAllDto();
                    if (AlePrints == null || AlePrints.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AlePrintReponse>>.Success(AlePrints, "Print Auditoria obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las Print Auditorias. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva Print Auditoria", Description = "Crea una nueva Print Auditoria en el sistema.")]
        public async Task<IActionResult> Add([FromBody] AlePrintRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var AlePrint = await _AlePrintService.Add(request);
                return StatusCode(201, Response<AlePrintReponse>.Created(AlePrint, "Print Auditoria creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Print Auditoria. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Print Auditoria", Description = "Actualiza la información de una Print Auditoria existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] AlePrintRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _AlePrintService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<AlePrintReponse>.NotFound("Print Auditoria no encontrada."));
                }
                saveDto.IdPrint = id;
                await _AlePrintService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Print Auditoria actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Print Auditoria. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Print Auditoria", Description = "Elimina una Print Auditoria de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var AlePrint = await _AlePrintService.GetByIdResponse(id);
                if (AlePrint == null)
                {
                    return NotFound(Response<string>.NotFound("Print Auditoria no encontrada."));
                }

                await _AlePrintService.Delete(id);
                return Ok(Response<string>.Success(null, "Print Auditoria eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Print Auditoria. Por favor, intente nuevamente."));
            }
        }
    }
}
