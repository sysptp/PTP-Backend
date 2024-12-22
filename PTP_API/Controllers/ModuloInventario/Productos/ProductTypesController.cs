using BussinessLayer.FluentValidations.ModuloInventario.Productos;
using BussinessLayer.FluentValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using System.Net.Mime;
using BussinessLayer.Wrappers;
using FluentValidation;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;

namespace PTP_API.Controllers.ModuloInventario.Productos
{
    [ApiController]
    [SwaggerTag("Gestión Tipos de Productos")]
    [Authorize]
    public class ProductTypesController : Controller
    {
        #region Propiedades
        private readonly ITipoProductoService _tipoProductosService;
        private readonly IValidator<CreateTipoProductoDto> _validatorCreate;
        private readonly IValidator<EditProductTypeDto> _validatorEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;

        public ProductTypesController(ITipoProductoService tipoProductosService,
            IValidator<CreateTipoProductoDto> validationCreate,
            IValidator<EditProductTypeDto> validatorEdit,
            IValidator<string> validateString,
            IValidator<long> validateNumbers
            )
        {
            _tipoProductosService = tipoProductosService;
            _validatorCreate = validationCreate;
            _validatorEdit = validatorEdit;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerTipoProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener tipo producto", Description = "Obtiene el tipo producto especifico por su id.")]
        public async Task<IActionResult> Get([FromQuery] int type)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(type);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var productoType = await _tipoProductosService.GetProductTypeById(type);
                if (productoType == null)
                {
                    return NotFound(Response<ViewProductTypeDto>.NotFound("Tipo producto no encontrado."));
                }

                return Ok(Response<ViewProductTypeDto>.Success(productoType, "Producto encontrado."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener productos", Description = "Obtiene todos los productos de una empresa.")]
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

                var type = await _tipoProductosService.GetAllProductsTypeByComp(idCompany);
                if (type == null || type.Count == 0)
                {
                    return Ok(Response<List<ViewProductTypeDto>>.NoContent("No hay tipo productos disponibles."));
                }

                return Ok(Response<List<ViewProductTypeDto>>.Success(type, "Productos obtenidos correctamente."));

            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los tipos productos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearTipoProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Tipo Producto", Description = "Endpoint para crear tipo producto")]
        public async Task<IActionResult> Add(CreateTipoProductoDto createType)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(createType);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var createdType = await _tipoProductosService.CreateProductType(createType);

                return Ok(Response<int?>.Created(createdType));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el tipo producto. Por favor, intente nuevamente."));
            }

        }

        [HttpPut("api/v1/[controller]/EditarTipoProducto")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Tipo Producto", Description = "Endpoint para editar tipo producto")]
        public async Task<IActionResult> Edit([FromBody] EditProductTypeDto edit)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _tipoProductosService.GetProductTypeById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Tipo producto no encontrado."));
                }

                await _tipoProductosService.EditProductType(edit);

                return Ok(Response<string>.Success("Tipo producto editado correctamente"));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el tipo producto. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarTipoProductoId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Tipo Producto", Description = "Endpoint para eliminar tipo producto por id")]
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

                await _tipoProductosService.DeleteProductTypeById(id);

                return Ok(Response<string>.Success("Tipo Producto eliminado correctamente"));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el tipo producto. Por favor, intente nuevamente."));
            }

        }
    }
}
