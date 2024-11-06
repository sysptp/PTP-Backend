using BussinessLayer.DTOs.ModuloInventario;
using BussinessLayer.FluentValidations.Productos;
using BussinessLayer.Interfaces.ModuloInventario;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloInventario
{
    [ApiController]
    [Route("api/v1/Products")]
    [SwaggerTag("Gestión de Productos")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly ProductosRequestValidator _validator;

        public ProductsController(IProductoService productoService, ProductosRequestValidator validations)
        {
            _productoService = productoService;
            _validator = validations;
        }

        [HttpGet("/ObtenerProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener productos", Description = "Obtiene una lista de todos los productos " +
            "dentro de una empresa o un producto específico de una empresa si se proporciona un codigo de producto.")]
        public async Task<IActionResult> Get([FromQuery] string? codeProduct, long idCompany)
        {
            try
            {
                if (idCompany == 0 || idCompany == null)
                {
                    return Ok(Response<string>.NotFound("El id de la empresa no puede ser nulo o 0"));

                }

                if (codeProduct != null)
                {
                    var producto = await _productoService.GetProductByCodeInCompany(codeProduct, idCompany);
                    if (producto == null)
                    {
                        return NotFound(Response<ViewProductsDto>.NotFound("Producto no encontrado."));
                    }

                    return Ok(Response<ViewProductsDto>.Success(producto, "Producto encontrado."));
                }
                else
                {
                    var productos = await _productoService.GetProductByIdCompany(idCompany);
                    if (productos == null || productos.Count == 0)
                    {
                        return Ok(Response<List<ViewProductsDto>>.NoContent("No hay Productos disponibles."));
                    }

                    return Ok(Response<List<ViewProductsDto>>.Success(productos, "Productos obtenidos correctamente."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("/CrearProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Producto", Description = "Endpoint para crear producto")]
        public async Task<IActionResult> Add([FromQuery] CreateProductsDto createProducts)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(createProducts);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var createdProdut = await _productoService.CreateProduct(createProducts);

                return Ok(Response<CreateProductsDto>.Created(createdProdut));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear la empresa. Por favor, intente nuevamente."));
            }

        }

        [HttpGet("/VerificarProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Verificar producto", Description = "Verifica si un producto existe por su codigo y la empresa")]
        public async Task<IActionResult> CheckCode([FromQuery] string productCode, long idEmpresa)
        {
            try
            {
                if (productCode == null || idEmpresa == null || idEmpresa == 0)
                {
                    return Ok(Response<bool>.NotFound("Los parametros no pueden ser nulos."));
                }

                var producto = await _productoService.CheckCodeExist(productCode, idEmpresa);

                if (producto)
                {
                    return Ok(Response<bool>.Success(producto, "Producto encontrado."));
                }
                else
                {
                    return Ok(Response<bool>.Success(producto, "Producto no encontrado."));
                }
                
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los productos. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("/ProductosFacturados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Facturacion de productos", Description = "Obtiene facturacion de productos por empresa")]
        public async Task<IActionResult> AllFacturacion([FromQuery] long idEmpresa)
        {
            try
            {
                if (idEmpresa == 0 || idEmpresa == null)
                {
                    return Ok(Response<string>.NotFound("El id de la empresa no puede ser nulo o 0"));

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

        [HttpGet("/ProductosCodigoBarra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener producto por codigo de barra", Description = "Obtiene un producto por codigo de barra y su empresa")]
        public async Task<IActionResult> ProductByBarCode([FromQuery] string? barCodeProduct, long idCompany)
        {
            try
            {
                if (barCodeProduct == null || idCompany == null || idCompany == 0)
                {
                    return Ok(Response<bool>.NotFound("Los parametros no pueden ser nulos."));
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

        [HttpGet("/ProductosFactura")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener producto por codigo de barra factura", Description = "Servicio para obtener productos por el código de barra de la factura")]
        public async Task<IActionResult> ProductByBarCodeFactura([FromQuery] string? barCodefactura, long idCompany)
        {
            try
            {
                if (barCodefactura == null || idCompany == null || idCompany == 0)
                {
                    return Ok(Response<bool>.NotFound("Los parametros no pueden ser nulos."));
                }

                var producto = await _productoService.GetProductoByBarCodeFactura(idCompany, barCodefactura);

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

        [HttpGet("/ProductosAgotados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener productos agotados", Description = "Servicio para obtener todos los productos agotados")]
        public async Task<IActionResult> AllAgotados([FromQuery] long idEmpresa)
        {
            try
            {
                if (idEmpresa == 0 || idEmpresa == null)
                {
                    return Ok(Response<string>.NotFound("El id de la empresa no puede ser nulo o 0"));

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
    }
}
