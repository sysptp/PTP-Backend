using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Archivos;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Archivos
{
    [ApiController]
    [SwaggerTag("Gestión de Parámetros de Archivo Subido")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class GnUploadFileParametroController : ControllerBase
    {
        private readonly IGnUploadFileParametroService _service;
        private readonly IValidator<CreateGnUploadFileParametroDto> _validatorCreate;
        private readonly IValidator<EditGnUploadFileParametroDto> _validatorEdit;
        private readonly IValidator<int> _validateNumbers;

        public GnUploadFileParametroController(
            IGnUploadFileParametroService service,
            IValidator<CreateGnUploadFileParametroDto> validatorCreate,
            IValidator<EditGnUploadFileParametroDto> validatorEdit,
            IValidator<int> validateNumbers)
        {
            _service = service;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener Parámetro por ID", Description = "Obtiene un parámetro de archivo subido específico por su ID.")]
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
                if (result == null) return NotFound(Response<string>.NotFound("Parámetro no encontrado."));
                return Ok(Response<ViewGnUploadFileParametroDto>.Success(result, "Parámetro encontrado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener Todos los Parámetros", Description = "Obtiene una lista de todos los parámetros de archivo subido.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return Ok(Response<List<ViewGnUploadFileParametroDto>>.Success(result, "Parámetros encontrados."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear Parámetro", Description = "Crea un nuevo parámetro de archivo subido.")]
        public async Task<IActionResult> Create([FromBody] CreateGnUploadFileParametroDto dto)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var id = await _service.Add(dto);
                return CreatedAtAction(nameof(GetById), new { id }, Response<int>.Success(id, "Parámetro creado exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Actualizar Parámetro", Description = "Actualiza un parámetro de archivo subido existente.")]
        public async Task<IActionResult> Update([FromBody] EditGnUploadFileParametroDto dto)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _service.Update(dto);
                return Ok(Response<string>.Success("Parámetro actualizado exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar Parámetro por ID", Description = "Elimina un parámetro de archivo subido por su ID.")]
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
                return Ok(Response<string>.Success("Parámetro eliminado exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }
    }

}
