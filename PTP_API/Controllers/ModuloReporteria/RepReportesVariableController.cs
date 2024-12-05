using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.Interfaces.ModuloReporteria;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloReporteria
{
    [ApiController]
    [SwaggerTag("Gestión de Variables de Reportes")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class RepReportesVariableController : ControllerBase
    {
        private readonly IRepReportesVariableService _service;
        private readonly IValidator<CreateRepReportesVariableDto> _validatorCreate;
        private readonly IValidator<EditRepReportesVariableDto> _validatorEdit;
        private readonly IValidator<long> _validateNumbers;

        public RepReportesVariableController(
            IRepReportesVariableService service,
            IValidator<CreateRepReportesVariableDto> validatorCreate,
            IValidator<EditRepReportesVariableDto> validatorEdit,
            IValidator<long> validateNumbers)
        {
            _service = service;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener Reporte Variables", Description = "Obtiene variables por id.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var result = await _service.GetById(id);
                if (result == null) return NotFound(Response<string>.NotFound("Variable no encontrada."));
                return Ok(Response<ViewRepReportesVariableDto>.Success(result, "Variable encontrada."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener Todos Reporte Variables", Description = "Obtiene todas las variables.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(Response<List<ViewRepReportesVariableDto>>.Success(result, "Variables encontradas."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear variable", Description = "Crea variables")]
        public async Task<IActionResult> Create([FromBody] CreateRepReportesVariableDto dto)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400));

                var id = await _service.Add(dto);
                return CreatedAtAction(nameof(GetById), new { id }, Response<int>.Success(id, "Variable creada exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Editar variable", Description = "Edita variables")]
        public async Task<IActionResult> Update([FromBody] EditRepReportesVariableDto dto)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(dto);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList(), 400));

                await _service.Update(dto);
                return Ok(Response<string>.Success("Variable actualizada exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar variable", Description = "Eliminar variables")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _service.Delete(id);
                return Ok(Response<string>.Success("Variable eliminada exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }
    }
}
