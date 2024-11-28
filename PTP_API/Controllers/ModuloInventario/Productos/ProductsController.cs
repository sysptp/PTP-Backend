using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.FluentValidations;
using BussinessLayer.FluentValidations.ModuloInventario.Productos;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;


[ApiController]
[SwaggerTag("Gestión de Productos")]
[Authorize]
public class ProductsController : ControllerBase
{
    #region Propiedades
    private readonly IProductoService _productoService;
    private readonly IValidator<CreateProductsDto> _validatorCreate;
    private readonly IValidator<EditProductDto> _validatorEdit;
    private readonly IValidator<long> _validateNumbers;
    private readonly IValidator<string> _validateString;

    public ProductsController(IProductoService productoService,
        IValidator<CreateProductsDto> validationCreate,
        IValidator<EditProductDto> validatorEdit,
        IValidator<string> validateString,
        IValidator<long> validateNumbers
        )
    {
        _productoService = productoService;
        _validatorCreate = validationCreate;
        _validatorEdit = validatorEdit;
        _validateString = validateString;
        _validateNumbers = validateNumbers;
    }
    #endregion

    [HttpGet("api/v1/[controller]/ObtenerProducto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener producto", Description = "Obtiene el producto especifico por su codigo dentro de una empresa.")]
    public async Task<IActionResult> Get([FromQuery] string codeProduct, long idCompany)
    {
        try
        {

            var validationResult = await _validateNumbers.ValidateAsync(idCompany);
            var validationResult2 = await _validateString.ValidateAsync(codeProduct);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            if (!validationResult2.IsValid)
            {
                var errors = validationResult2.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var producto = await _productoService.GetProductByCodeInCompany(codeProduct, idCompany);
            if (producto == null)
            {
                return NotFound(Response<ViewProductsDto>.NotFound("Producto no encontrado."));
            }

            return Ok(Response<ViewProductsDto>.Success(producto, "Producto encontrado."));
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
        }
    }

    [HttpGet("api/v1/[controller]/ObtenerProductos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener productos", Description = "Obtiene todos los productos de una empresa.")]
    public async Task<IActionResult> GetAllProductsByComp([FromQuery] long idCompany)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idCompany);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var productos = await _productoService.GetProductByIdCompany(idCompany);
            if (productos == null || productos.Count == 0)
            {
                return Ok(Response<List<ViewProductsDto>>.NoContent("No hay Productos disponibles."));
            }

            return Ok(Response<List<ViewProductsDto>>.Success(productos, "Productos obtenidos correctamente."));
            
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
        }
    }

    [HttpGet("api/v1/[controller]/ProductosFacturados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener Facturacion de productos", Description = "Obtiene todos los productos vendidos por una empresa")]
    public async Task<IActionResult> AllFacturacion([FromQuery] long idEmpresa)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var producto = await _productoService.GetAllFacturacion(idEmpresa);

            if (producto.Count > 0)
            {
                return Ok(Response<List<ViewProductsDto>>.Success(producto, "Productos encontrados."));
            }
            else
            {
                return Ok(Response<List<ViewProductsDto>>.Success(producto, "No existen productos facturados."));
            }
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
        }
    }

    [HttpGet("api/v1/[controller]/ProductosCodigoBarra")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener producto por codigo de barra", Description = "Obtiene un producto por codigo de barra y su empresa")]
    public async Task<IActionResult> ProductByBarCode([FromQuery] string barCodeProduct, long idCompany)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idCompany);
            var validationResult2 = await _validateString.ValidateAsync(barCodeProduct);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            if (!validationResult2.IsValid)
            {
                var errors = validationResult2.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var producto = await _productoService.GetProductoByBarCode(idCompany, barCodeProduct);

            if (producto != null)
            {


                return Ok(Response<ViewProductsDto>.Success(producto, "Producto encontrado."));
            }
            else
            {
                return Ok(Response<ViewProductsDto>.NoContent("Producto no encontrado."));

            }
        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
        }
    }

    [HttpGet("api/v1/[controller]/ProductosAgotados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Obtener productos agotados", Description = "Servicio para obtener todos los productos agotados")]
    public async Task<IActionResult> AllAgotados([FromQuery] long idEmpresa)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var producto = await _productoService.GetAllAgotados(idEmpresa);

            if (producto.Count > 0)
            {
                return Ok(Response<List<ViewProductsDto>>.Success(producto, "Productos agotados encontrados."));
            }
            else
            {
                return Ok(Response<List<ViewProductsDto>>.Success(producto, "No existen productos agotados."));
            }

        }
        catch
        {
            return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos agotados. Por favor, intente nuevamente."));
        }
    }

    [HttpPost("api/v1/[controller]/CrearProducto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Crear Producto", Description = "Endpoint para crear producto")]
    public async Task<IActionResult> Add(CreateProductsDto createProducts)
    {
        try
        {
            var validationResult = await _validatorCreate.ValidateAsync(createProducts);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var createdProdut = await _productoService.CreateProduct(createProducts);

            return Ok(Response<int?>.Created(createdProdut));

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());

            return Ok(Response<string>.ServerError("Ocurrió un error al crear la empresa. Por favor, intente nuevamente."));
        }

    }

    [HttpPut("api/v1/[controller]/EditarProducto")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Editar Producto", Description = "Endpoint para editar producto")]
    public async Task<IActionResult> EditProduct([FromBody] EditProductDto editProducts)
    {
        try
        {
            var validationResult = await _validatorEdit.ValidateAsync(editProducts);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var existingProducts = await _productoService.GetProductById(editProducts.Id);
            if (existingProducts == null)
            {
                return NotFound(Response<string>.NotFound("Producto no encontrado."));
            }

            await _productoService.EditProduct(editProducts);

            return Ok(Response<string>.Success("Producto editado correctamente"));

        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al editar el producto. Por favor, intente nuevamente."));
        }

    }

    [HttpDelete("api/v1/[controller]/EliminarProductoCodigo/{codigo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(Summary = "Eliminar Producto", Description = "Endpoint para eliminar producto por codigo")]
    public async Task<IActionResult> DeleteByCode(string codigo, long idEmpresa)
    {
        try
        {
            var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);
            var validationResult2 = await _validateString.ValidateAsync(codigo);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            if (!validationResult2.IsValid)
            {
                var errors = validationResult2.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            await _productoService.DeleteProductByCodigo(codigo, idEmpresa);

            return Ok(Response<string>.Success(codigo));

        }
        catch
        {

            return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el producto. Por favor, intente nuevamente."));
        }

    }

    //[ApiExplorerSettings(IgnoreApi = true)]
    //[HttpGet("api/v1/[controller]/ProductosFactura")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[SwaggerOperation(Summary = "Obtener producto por codigo de barra factura", Description = "Servicio para obtener productos por el código de barra de la factura")]
    //public async Task<IActionResult> ProductByBarCodeFactura([FromQuery] string? barCodefactura, long idCompany)
    //{
    //    try
    //    {
    //        if (barCodefactura == null || idCompany == null || idCompany == 0)
    //        {
    //            return Ok(Response<bool>.NotFound("Los parametros no pueden ser nulos."));
    //        }

    //        var producto = await _productoService.GetProductoByBarCodeFactura(idCompany, barCodefactura);

    //        if (producto != null)
    //        {


    //            return Ok(Response<ViewProductsDto>.Success(producto, "Producto encontrado."));
    //        }
    //        else
    //        {
    //            return Ok(Response<ViewProductsDto>.NoContent("Producto no encontrado."));

    //        }

    //    }
    //    catch
    //    {
    //        return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
    //    }
    //}

    //[ApiExplorerSettings(IgnoreApi = true)]
    //[HttpGet("api/v1/[controller]/VerificarProducto")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[SwaggerOperation(Summary = "Verificar producto", Description = "Verifica si un producto existe por su codigo y la empresa")]
    //public async Task<IActionResult> CheckCode([FromQuery] string productCode, long idEmpresa)
    //{
    //    try
    //    {
    //        if (productCode == null || idEmpresa == null || idEmpresa == 0)
    //        {
    //            return Ok(Response<bool>.NotFound("Los parametros no pueden ser nulos."));
    //        }

    //        var producto = await _productoService.CheckCodeExist(productCode, idEmpresa);

    //        if (producto)
    //        {
    //            return Ok(Response<bool>.Success(producto, "Producto encontrado."));
    //        }
    //        else
    //        {
    //            return Ok(Response<bool>.Success(producto, "Producto no encontrado."));
    //        }

    //    }
    //    catch
    //    {
    //        return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
    //    }
    //}
}




