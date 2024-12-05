using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using System.Net.Mime;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using FluentValidation;

namespace PTP_API.Controllers.ModuloInventario.Impuestos
{
    [ApiController]
    [SwaggerTag("Gestión de Impuestos")]
    [Authorize]
    public class TaxController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateTaxDto> _validatorCreate;
        private readonly IValidator<EditTaxDto> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IImpuestosService _impuestosService;

        public TaxController(
            IImpuestosService impuestosService,
            IValidator<CreateTaxDto> validationRules,
            IValidator<EditTaxDto> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _impuestosService = impuestosService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerImpuesto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Impuesto", Description = "Obtiene un impuesto en especifico por su id.")]
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

                var data = await _impuestosService.GetTaxById(id);

                if (data != null)
                {

                    return Ok(Response<ViewTaxDto>.Success(data, "Impuesto encontrado."));
                }
                else
                {
                    return Ok(Response<ViewTaxDto>.NotFound("No se han encontrado Impuestos."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener el impuesto. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerImpuestos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Impuestos", Description = "Obtiene todas los impuestos de una empresa por su id.")]
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

                var data = await _impuestosService.GetTaxByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewTaxDto>>.Success(data, "Impuestos encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewTaxDto>>.NotFound("No se han encontrado impuestos."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los impuestos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearImpuesto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Impuesto", Description = "Endpoint para crear impuesto.")]
        public async Task<IActionResult> Add(CreateTaxDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _impuestosService.CreateTax(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el impuesto. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarImpuesto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Impuesto", Description = "Endpoint para editar el impuesto")]
        public async Task<IActionResult> Edit([FromBody] EditTaxDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _impuestosService.GetTaxById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Impuesto no encontrado."));
                }

                await _impuestosService.EditTax(edit);


                return Ok(Response<string>.Success("Impuesto editado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el impuesto. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarImpuestoId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Impuesto", Description = "Endpoint para eliminar impuestos por id")]
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

                await _impuestosService.DeleteTaxById(id);

                return Ok(Response<int>.Success(id, "Impuesto eliminado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el impuesto. Por favor, intente nuevamente."));
            }

        }
    }
}
