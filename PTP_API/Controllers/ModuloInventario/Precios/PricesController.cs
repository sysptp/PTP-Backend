using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.FluentValidations.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Precios;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


[ApiController]
[SwaggerTag("Gestión de Precios")]
[Authorize]
public class PricesController : Controller
{
    #region Propiedades
    private readonly IPrecioService _precioService;
    private readonly CreatePreciosRequestValidator _validatorCreate;
    private readonly EditPreciosRequestValidator _validationsEdit;

    public PricesController(IPrecioService precioService,
        CreatePreciosRequestValidator validationRules, 
        EditPreciosRequestValidator validations) {
        _precioService = precioService; 
        _validatorCreate = validationRules;
        _validationsEdit = validations;
    }
    #endregion

    [HttpGet("api/v1/[controller]/ObtenerPrecio/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener Precio", Description = "Obtiene un precio en especifico por su id.")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            if (id == 0 || id == null)
            {
                return Ok(Response<string>.NotFound("El id del precio no puede ser nulo o 0"));

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
            if (idProduct == 0 || idProduct == null)
            {
                return Ok(Response<string>.NotFound("El id del producto no puede ser nulo o 0"));

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

            return Ok(Response<CreatePreciosDto>.Created(createdPrice));

        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al crear el precio. Por favor, intente nuevamente."));
        }

    }

    [HttpPost("api/v1/[controller]/EditarPrecio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Editar Precio", Description = "Endpoint para editar Precio")]
    public async Task<IActionResult> EditProduct([FromQuery] EditPricesDto editPrecios)
    {
        try
        {
            var validationResult = await _validationsEdit.ValidateAsync(editPrecios);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var editedPrecios = await _precioService.EditPrice(editPrecios);

            return Ok(Response<EditPricesDto>.Success(editedPrecios, "Precio editado correctamente"));

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
            if (id == 0 || id == null)
            {
                return Ok(Response<string>.NotFound("El id del precio no puede ser nulo o 0"));

            }

            await _precioService.DeletePriceById(id);

            return Ok(Response<int>.Success(id, "Precio eliminado correctamente"));
        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el precio. Por favor, intente nuevamente."));
        }

    }

    [HttpPost("api/v1/[controller]/SetSamePrice")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Asignar mismo precio", Description = "Endpoint para asignar el mismo precio a una lista de productos")]
    public async Task<IActionResult> SamePrice(List<EditPricesDto> editPrecios, decimal newPrice)
    {
        try
        {
            foreach (EditPricesDto price in editPrecios)
            {
                var validationResult = await _validationsEdit.ValidateAsync(price);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }
            }

            await _precioService.SetSamePrice(editPrecios, newPrice);

            return Ok(Response<string>.Success("Precios actualizados correctamente"));
        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al editar el precio. Por favor, intente nuevamente."));
        }
    }
}

