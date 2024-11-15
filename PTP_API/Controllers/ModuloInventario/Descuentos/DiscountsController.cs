using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.FluentValidations.ModuloInventario.Impuestos;
using BussinessLayer.FluentValidations;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.FluentValidations.ModuloInventario.Descuentos;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloInventario.Descuentos;

namespace PTP_API.Controllers.ModuloInventario.Descuentos
{
    [ApiController]
    [SwaggerTag("Gestión de Descuentos")]
    [Authorize]
    public class DiscountsController : ControllerBase
    {
        #region Propiedades
        private readonly CreateDiscountRequestValidation _validatorCreate;
        private readonly EditDiscountRequestValidation _validationsEdit;
        private readonly NumbersRequestValidator _validateNumbers;
        private readonly StringsRequestValidator _validateString;
        private readonly IDescuentoService _descuentosService;

        public DiscountsController(
            IDescuentoService descuentosService,
            CreateDiscountRequestValidation validationRules,
            EditDiscountRequestValidation validations,
            StringsRequestValidator validateString,
            NumbersRequestValidator validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _descuentosService = descuentosService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerDescuento/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Descuento", Description = "Obtiene un descuento en especifico por su id.")]
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

                var data = await _descuentosService.GetDiscountById(id);

                if (data != null)
                {

                    return Ok(Response<ViewDiscountDto>.Success(data, "Discuento encontrado."));
                }
                else
                {
                    return Ok(Response<ViewDiscountDto>.NotFound("No se han encontrado Descuento."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener el impuesto. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerDescuentos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Descuentos", Description = "Obtiene todas los descuento de una empresa por su id.")]
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

                var data = await _descuentosService.GetDiscountByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewDiscountDto>>.Success(data, "Decuentos encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewDiscountDto>>.NotFound("No se han encontrado descuentos."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los impuestos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearDescuento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Descuento", Description = "Endpoint para crear descuentos.")]
        public async Task<IActionResult> Add(CreateDiscountDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _descuentosService.CreateDiscount(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el descuento. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarImpuesto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Descuento", Description = "Endpoint para editar el descuento")]
        public async Task<IActionResult> Edit([FromBody] EditDiscountDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _descuentosService.GetDiscountById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Descuento no encontrado."));
                }

                await _descuentosService.EditDiscount(edit);


                return Ok(Response<string>.Success("Descuento editado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el descuento. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarDescuentoId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Descuento", Description = "Endpoint para eliminar descuento por id")]
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

                await _descuentosService.DeleteDiscountById(id);

                return Ok(Response<int>.Success(id, "Descuento eliminado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el descuento. Por favor, intente nuevamente."));
            }

        }
    }
}
