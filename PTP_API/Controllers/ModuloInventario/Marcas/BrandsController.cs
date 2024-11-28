using BussinessLayer.FluentValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using BussinessLayer.FluentValidations.ModuloInventario.Marcas;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloInventario.Marcas;
using FluentValidation;

namespace PTP_API.Controllers.ModuloInventario.Marcas
{
    [ApiController]
    [SwaggerTag("Gestión de Marcas")]
    [Authorize]
    public class BrandsController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateBrandDto> _validatorCreate;
        private readonly IValidator<EditBrandDto> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IMarcaService _marcaService;

        public BrandsController(
            IMarcaService marcaService,
            IValidator<CreateBrandDto> validationRules,
            IValidator<EditBrandDto> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _marcaService = marcaService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerMarca/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Marca", Description = "Obtiene una marca en especifico por su id.")]
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

                var data = await _marcaService.GetBrandById(id);

                if (data != null)
                {

                    return Ok(Response<ViewBrandDto>.Success(data, "Marca encontrada."));
                }
                else
                {
                    return Ok(Response<ViewBrandDto>.NotFound("No se han encontrado marcas."));
                }
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la marca. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerMarcas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Marcas", Description = "Obtiene todas las marcas de una empresa por su id.")]
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

                var data = await _marcaService.GetBrandsByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewBrandDto>>.Success(data, "Marcas encontradas."));
                }
                else
                {
                    return Ok(Response<List<ViewBrandDto>>.NotFound("No se han encontrado marcas."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener las marcas. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearMarca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Marca", Description = "Endpoint para crear marcas")]
        public async Task<IActionResult> Add(CreateBrandDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _marcaService.CreateBrand(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear la marca. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarMarca")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Marca", Description = "Endpoint para editar Marca")]
        public async Task<IActionResult> Edit([FromBody] EditBrandDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _marcaService.GetBrandById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Marca no encontrada."));
                }

                await _marcaService.EditBrand(edit);


                return Ok(Response<string>.Success("Marca editada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar la marca. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarMarcaId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Marca", Description = "Endpoint para eliminar marca por id")]
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

                await _marcaService.DeleteBrandById(id);

                return Ok(Response<int>.Success(id, "Marca eliminada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar la marca. Por favor, intente nuevamente."));
            }

        }
    }
}
