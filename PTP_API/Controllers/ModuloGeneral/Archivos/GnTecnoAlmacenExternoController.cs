using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.ModuloGeneral.Archivos;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Archivos
{
    [ApiController]
    [SwaggerTag("Gestión de Almacenamiento Externo")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class GnTecnoAlmacenExternoController : ControllerBase
    {
        private readonly IGnTecnoAlmacenExternoService _service;
        private readonly IValidator<CreateGnTecnoAlmacenExternoDto> _validatorCreate;
        private readonly IValidator<EditGnTecnoAlmacenExternoDto> _validatorEdit;
        private readonly IValidator<long> _validateNumbers;

        public GnTecnoAlmacenExternoController(
            IGnTecnoAlmacenExternoService service,
            IValidator<CreateGnTecnoAlmacenExternoDto> validatorCreate,
            IValidator<EditGnTecnoAlmacenExternoDto> validatorEdit,
            IValidator<long> validateNumbers)
        {
            _service = service;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }

        // GET: api/v1/GnTecnoAlmacenExterno/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener Almacen por ID", Description = "Obtiene un almacenamiento externo por ID.")]
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
                if (result == null) return NotFound(Response<string>.NotFound("Almacen no encontrado."));
                return Ok(Response<ViewGnTecnoAlmacenExternoDto>.Success(result, "Almacen encontrado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        // Obtener almacenes externos por empresa
        [HttpGet("ObtenerPoEmpresaId/{idEmpresa}")]
        [SwaggerOperation(Summary = "Obtiene almacenes externos por empresa", Description = "Devuelve una lista de almacenes externos activos de una empresa específica.")]
        public async Task<ActionResult<List<ViewGnTecnoAlmacenExternoDto>>> GetByCompany(int idEmpresa)
        {
            // Validación del ID de empresa
            var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var result = await _service.GetByCompany(idEmpresa);
            if (result == null || !result.Any())
            {
                return NotFound(new { Message = "No se encontraron almacenes externos para esta empresa." });
            }

            return Ok(result);
        }

        // POST: api/v1/GnTecnoAlmacenExterno
        [HttpPost]
        [SwaggerOperation(Summary = "Crear Almacen Externo", Description = "Crea un nuevo almacenamiento externo.")]
        public async Task<IActionResult> Add([FromBody] CreateGnTecnoAlmacenExternoDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _service.Add(create);
                return Ok(Response<int>.Created(created));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        // PUT: api/v1/GnTecnoAlmacenExterno
        [HttpPut]
        [SwaggerOperation(Summary = "Editar Almacen Externo", Description = "Edita un almacenamiento externo existente.")]
        public async Task<IActionResult> Edit([FromBody] EditGnTecnoAlmacenExternoDto edit)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(edit);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _service.Update(edit);
                return Ok(Response<string>.Success("Almacen Externo editado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }

        // DELETE: api/v1/GnTecnoAlmacenExterno/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar Almacen Externo", Description = "Elimina un almacenamiento externo.")]
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
                return Ok(Response<string>.Success("Almacen Externo eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Error interno: " + ex.Message));
            }
        }
    }

}
