using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.Interfaces.Services.ModuloReporteria;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloReporteria
{
    [ApiController]
    [SwaggerTag("Gestión de Reportes")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class RepReporteController : ControllerBase
    {
        private readonly IRepReporteService _service;
        private readonly IValidator<CreateRepReporteDto> _validatorCreate;
        private readonly IValidator<EditRepReporteDto> _validatorEdit;
        private readonly IValidator<long> _validateNumbers;

        public RepReporteController(
            IRepReporteService service,
            IValidator<CreateRepReporteDto> validatorCreate,
            IValidator<EditRepReporteDto> validatorEdit,
            IValidator<long> validateNumbers)
        {
            _service = service;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }

        [HttpGet("ObtenerPorId/{id}")]
        [SwaggerOperation(Summary = "Obtener Reporte", Description = "Obtiene un reporte en especifico por su id.")]
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

                var data = await _service.GetById(id);
                if (data == null)
                    return NotFound(Response<string>.NotFound("Reporte no encontrado."));

                return Ok(Response<ViewRepReporteDto>.Success(data, "Reporte encontrado."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al obtener el reporte: {ex.Message}"));
            }
        }

        [HttpGet("ObtenerTodos")]
        [SwaggerOperation(Summary = "Obtener Reporte", Description = "Obtiene todos los reportes.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAll();
                return Ok(Response<List<ViewRepReporteDto>>.Success(data, "Listado de reportes."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al obtener el listado: {ex.Message}"));
            }
        }

        [HttpPost("Crear")]
        [SwaggerOperation(Summary = "Crear Reporte", Description = "Crea un reporte")]
        public async Task<IActionResult> Add(CreateRepReporteDto model)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList()));

                var createdId = await _service.Add(model);
                return Ok(Response<int>.Created(createdId));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al crear el reporte: {ex.Message}"));
            }
        }

        [HttpPut("Editar")]
        [SwaggerOperation(Summary = "Editar Reporte", Description = "Edita un reporte.")]
        public async Task<IActionResult> Edit(EditRepReporteDto model)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage).ToList()));

                var existing = await _service.GetById(model.Id);
                if (existing == null)
                    return NotFound(Response<string>.NotFound("Reporte no encontrado."));

                await _service.Update(model);
                return Ok(Response<string>.Success("Reporte actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al actualizar el reporte: {ex.Message}"));
            }
        }

        [HttpDelete("Eliminar/{id}")]
        [SwaggerOperation(Summary = "Eliminar Reporte", Description = "Elimina un reporte.")]
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
                return Ok(Response<string>.Success("Reporte eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al eliminar el reporte: {ex.Message}"));
            }
        }
    }
}
