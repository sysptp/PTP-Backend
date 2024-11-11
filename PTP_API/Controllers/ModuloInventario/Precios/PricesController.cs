using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.FluentValidations;
using BussinessLayer.FluentValidations.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Precios;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

[ApiController]
[SwaggerTag("Gestión de Precios")]
[Authorize]
public class PricesController : Controller
{
    #region Propiedades
    private readonly IPrecioService _precioService;
    private readonly CreatePreciosRequestValidator _validatorCreate;
    private readonly EditPreciosRequestValidator _validationsEdit;
    private readonly NumbersRequestValidator _validateNumbers;
    private readonly StringsRequestValidator _validateString;

    public PricesController(IPrecioService precioService,
        CreatePreciosRequestValidator validationRules, 
        EditPreciosRequestValidator validations,
        StringsRequestValidator validateString,
        NumbersRequestValidator validateNumbers) {
        _precioService = precioService; 
        _validatorCreate = validationRules;
        _validationsEdit = validations;
        _validateString = validateString;
        _validateNumbers = validateNumbers;
    }
    #endregion

    [HttpGet("api/v1/[controller]/ObtenerPrecio/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener Precio", Description = "Obtiene un precio en especifico por su id.")]
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

            var precio = await _precioService.GetPriceById(id);

            if (precio != null)
            {

                return Ok(Response<ViewPreciosDto>.Success(precio, "Precio encontrado."));
            }
            else
            {
                return Ok(Response<ViewPreciosDto>.NotFound("No se han encontrado el precio."));
            }
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener el precio. Por favor, intente nuevamente."));
        }
    }

    [HttpGet("api/v1/[controller]/ObtenerPrecios")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener Precios", Description = "Obtiene todos los precios de un producto por su id.")]
    public async Task<IActionResult> Get([FromQuery] int idProduct)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idProduct);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var precio = await _precioService.GetPricesByIdProduct(idProduct);

            if (precio.Count > 0)
            {
                  
                return Ok(Response<List<ViewPreciosDto>>.Success(precio, "Precios encontrados."));
            }
            else
            {
                return Ok(Response<List<ViewPreciosDto>>.NotFound("No se han encontrado precios."));
            }
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los precios. Por favor, intente nuevamente."));
        }
    }

    [HttpPost("api/v1/[controller]/CrearPrecio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Crear Precio", Description = "Endpoint para crear precio")]
    public async Task<IActionResult> Add([FromQuery] CreatePreciosDto createPrecios)
    {
        try
        {
            var validationResult = await _validatorCreate.ValidateAsync(createPrecios);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var createdPrice = await _precioService.CreatePrices(createPrecios);

            return Ok(Response<int?>.Created(createdPrice));

        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al crear el precio. Por favor, intente nuevamente."));
        }
    }

    [HttpPut("api/v1/[controller]/SetSamePrice")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Asignar mismo precio", Description = "Endpoint para asignar el mismo precio a una lista de productos")]
    public async Task<IActionResult> SamePrice([FromBody] List<EditPricesDto> editPrecios, [FromQuery] decimal newPrice)
    {
        try
        {
            var validationNumber = await _validateNumbers.ValidateAsync(long.Parse(newPrice.ToString()));
            if (!validationNumber.IsValid)
            {
                var errors = validationNumber.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var allErrors = new List<string>();
            foreach (EditPricesDto price in editPrecios)
            {
                var validationResult = await _validationsEdit.ValidateAsync(price);
                if (!validationResult.IsValid)
                {
                    allErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                }
            }

            if (allErrors.Any())
            {
                return BadRequest(Response<string>.BadRequest(allErrors, 400));
            }

            await _precioService.SetSamePrice(editPrecios, newPrice);
            return Ok(Response<string>.Success("Precios actualizados correctamente"));
        }
        catch (Exception)
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al editar el precio. Por favor, intente nuevamente."));
        }
    }

    [HttpPut("api/v1/[controller]/EditarPrecio")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Editar Precio", Description = "Endpoint para editar Precio")]
    public async Task<IActionResult> EditProduct([FromBody] EditPricesDto editPrecios)
    {
        try
        {
            var validationResult = await _validationsEdit.ValidateAsync(editPrecios);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var existingPrice = await _precioService.GetPriceById(editPrecios.Id);
            if (existingPrice == null)
            {
                return NotFound(Response<string>.NotFound("Precio no encontrado."));
            }

            await _precioService.EditPrice(editPrecios);


            return Ok(Response<string>.Success("Precio editado correctamente"));
        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al editar el precio. Por favor, intente nuevamente."));
        }

    }

    [HttpDelete("api/v1/[controller]/EliminarProductoId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Eliminar Precio", Description = "Endpoint para eliminar precio por id")]
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

            await _precioService.DeletePriceById(id);

            return Ok(Response<int>.Success(id, "Precio eliminado correctamente"));
        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el precio. Por favor, intente nuevamente."));
        }

    }
}

