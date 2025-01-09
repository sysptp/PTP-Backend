using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral.Moneda
{
    [ApiController]
    [SwaggerTag("Gestión de Monedas")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateCurrencyDTO> _validatorCreate;
        private readonly IValidator<EditCurrencyDTO> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IMonedasService _monedasService;

        public CurrencyController(
            IMonedasService monedasService,
            IValidator<CreateCurrencyDTO> validationRules,
            IValidator<EditCurrencyDTO> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _monedasService = monedasService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerMoneda/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Moneda", Description = "Obtiene una moneda en especifico por su id.")]
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

                var data = await _monedasService.GetById(id);

                if (data != null)
                {

                    return Ok(Response<ViewCurrencyDTO>.Success(data, "Moneda encontrada."));
                }
                else
                {
                    return Ok(Response<ViewCurrencyDTO>.NotFound("No se han encontrado Monedas."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la moneda. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerMonedas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Monedas", Description = "Obtiene todas las monedas de una empresa por su id.")]
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

                var data = await _monedasService.GetByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewCurrencyDTO>>.Success(data, "Monedas encontradas."));
                }
                else
                {
                    return Ok(Response<List<ViewCurrencyDTO>>.NotFound("No se han encontrado monedas."));
                }
            }
            catch(Exception ex)
            {
                return Ok(Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("api/v1/[controller]/CrearMoneda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Moneda", Description = "Endpoint para crear moneda.")]
        public async Task<IActionResult> Add(CreateCurrencyDTO create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _monedasService.Add(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear la moneda. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarMoneda")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Moneda", Description = "Endpoint para editar moneda")]
        public async Task<IActionResult> Edit([FromBody] EditCurrencyDTO edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _monedasService.GetById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Moneda no encontrada."));
                }

                await _monedasService.Update(edit);


                return Ok(Response<string>.Success("Moneda editada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar la moneda. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarMonedaId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Moneda", Description = "Endpoint para eliminar moneda por id")]
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

                await _monedasService.Delete(id);

                return Ok(Response<int>.Success(id, "Moneda eliminada correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar la moneda. Por favor, intente nuevamente."));
            }

        }
    }
}
