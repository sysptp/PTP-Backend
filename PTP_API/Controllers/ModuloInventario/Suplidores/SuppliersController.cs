using BussinessLayer.FluentValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using BussinessLayer.FluentValidations.ModuloInventario.Suplidores;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Productos;

namespace PTP_API.Controllers.ModuloInventario.Suplidores
{
    [ApiController]
    [SwaggerTag("Gestión de Suplidores")]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateSuppliersDto> _validatorCreate;
        private readonly IValidator<EditSuppliersDto> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly ISuplidoresService _suplidoresService;

        public SuppliersController(
            ISuplidoresService suplidoresService,
            IValidator<CreateSuppliersDto> validationRules,
            IValidator<EditSuppliersDto> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _suplidoresService = suplidoresService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerSuplidor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Suplidor", Description = "Obtiene un suplidor en especifico por su id.")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _suplidoresService.GetById(id);

                if (data != null)
                {

                    return Ok(Response<ViewSuppliersDto>.Success(data, "Dato encontrado."));
                }
                else
                {
                    return Ok(Response<ViewSuppliersDto>.NotFound("No se han encontrado datos."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la data. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerSuplidores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Suplidores", Description = "Obtiene todas los suplidores de una empresa por su id.")]
        public async Task<IActionResult> GetAll([FromQuery] int idCompany)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idCompany);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _suplidoresService.GetByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewSuppliersDto>>.Success(data, "Datos encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewSuppliersDto>>.NotFound("No se han encontrado datos."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la data. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearSuplidor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Suplidor", Description = "Endpoint para crear Suplidor.")]
        public async Task<IActionResult> Add(CreateSuppliersDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _suplidoresService.Create(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el registro. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarSuplidor")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Suplidor", Description = "Endpoint para editar el suplidor")]
        public async Task<IActionResult> Update([FromBody] EditSuppliersDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _suplidoresService.GetById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Datos no encontrado."));
                }

                await _suplidoresService.Edit(edit);


                return Ok(Response<string>.Success("Registro editado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el registro. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarSuplidorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Suplidor", Description = "Endpoint para eliminar suplidor por id")]
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

                await _suplidoresService.DeleteById(id);

                return Ok(Response<int>.Success(id, "Registro eliminado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el registro. Por favor, intente nuevamente."));
            }

        }
    }
}
