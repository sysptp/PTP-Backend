using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Utils;
using BussinessLayer.Interfaces.Services.ModuloGeneral;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral
{
    [ApiController]
    [SwaggerTag("Gestión de unidades de repetición")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class RepeatUnitController : ControllerBase
    {
        private readonly IGnRepeatUnitService _repeatUnitService;
        private readonly IValidator<GnRepeatUnitRequest> _validator;

        public RepeatUnitController(IGnRepeatUnitService repeatUnitService, IValidator<GnRepeatUnitRequest> validator)
        {
            _repeatUnitService = repeatUnitService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnRepeatUnitResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener unidades de repetición", Description = "Devuelve una lista de unidades de repetición o una específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllRepeatUnits([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var repeatUnit = await _repeatUnitService.GetByIdResponse(id.Value);
                    if (repeatUnit == null)
                        return NotFound(Response<GnRepeatUnitResponse>.NotFound("Unidad de repetición no encontrada."));

                    return Ok(Response<GnRepeatUnitResponse>.Success(repeatUnit, "Unidad de repetición encontrada."));
                }
                else
                {
                    var repeatUnits = await _repeatUnitService.GetAllDto();
                    if (repeatUnits == null || !repeatUnits.Any())
                        return StatusCode(204, Response<IEnumerable<GnRepeatUnitResponse>>.NoContent("No hay unidades de repetición disponibles."));

                    return Ok(Response<IEnumerable<GnRepeatUnitResponse>>.Success(repeatUnits, "Unidades de repetición obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva unidad de repetición", Description = "Endpoint para registrar una unidad de repetición")]
        public async Task<IActionResult> CreateRepeatUnit([FromBody] GnRepeatUnitRequest repeatUnitDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(repeatUnitDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _repeatUnitService.Add(repeatUnitDto);
                return CreatedAtAction(nameof(GetAllRepeatUnits), response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una unidad de repetición", Description = "Endpoint para actualizar una unidad de repetición")]
        public async Task<IActionResult> UpdateRepeatUnit(int id, [FromBody] GnRepeatUnitRequest repeatUnitDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(repeatUnitDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingRepeatUnit = await _repeatUnitService.GetByIdRequest(id);
                if (existingRepeatUnit == null)
                    return NotFound(Response<string>.NotFound("Unidad de repetición no encontrada."));

                repeatUnitDto.Id = id;
                await _repeatUnitService.Update(repeatUnitDto, id);
                return Ok(Response<string>.Success(null, "Unidad de repetición actualizada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una unidad de repetición", Description = "Endpoint para eliminar una unidad de repetición")]
        public async Task<IActionResult> DeleteRepeatUnit(int id)
        {
            try
            {
                var existingRepeatUnit = await _repeatUnitService.GetByIdRequest(id);
                if (existingRepeatUnit == null)
                    return NotFound(Response<string>.NotFound("Unidad de repetición no encontrada."));

                await _repeatUnitService.Delete(id);
                return Ok(Response<string>.Success(null, "Unidad de repetición eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}