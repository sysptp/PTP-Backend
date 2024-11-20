using BussinessLayer.FluentValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using BussinessLayer.FluentValidations.ModuloInventario.Versiones;
using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;

namespace PTP_API.Controllers.ModuloInventario.Versiones
{
    [ApiController]
    [SwaggerTag("Gestión de Marcas")]
    [Authorize]
    public class VersionsController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateVersionsDto> _validatorCreate;
        private readonly IValidator<EditVersionsDto> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IVersionService _versionService;

        public VersionsController(
            IVersionService versionService,
            IValidator<CreateVersionsDto> validationRules,
            IValidator<EditVersionsDto> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _versionService = versionService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerVersion/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Version", Description = "Obtiene una version en especifico por su id.")]
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

                var data = await _versionService.GetVersionById(id);

                if (data != null)
                {

                    return Ok(Response<ViewVersionsDto>.Success(data, "Version encontrada."));
                }
                else
                {
                    return Ok(Response<ViewVersionsDto>.NotFound("No se han encontrado Versiones."));
                }
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la version. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerVersiones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Versiones", Description = "Obtiene todas las versiones de una empresa por su id.")]
        public async Task<IActionResult> Get([FromQuery] int idCompany)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idCompany);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _versionService.GetVersionByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewVersionsDto>>.Success(data, "Versiones encontradas."));
                }
                else
                {
                    return Ok(Response<List<ViewVersionsDto>>.NotFound("No se han encontrado versiones."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener las versiones. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearVersion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear version", Description = "Endpoint para crear version")]
        public async Task<IActionResult> Add(CreateVersionsDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _versionService.CreateVersion(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear la version. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarMarca")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Version", Description = "Endpoint para editar Version")]
        public async Task<IActionResult> Edit([FromBody] EditVersionsDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _versionService.GetVersionById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Version no encontrada."));
                }

                await _versionService.EditVersion(edit);


                return Ok(Response<string>.Success("Version editada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar la version. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarVersionId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Version", Description = "Endpoint para eliminar version por id")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _versionService.DeleteVersionById(id);

                return Ok(Response<int>.Success(id, "Version eliminada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar la version. Por favor, intente nuevamente."));
            }

        }
    }
}
