using BussinessLayer.DTOs.Productos;
using BussinessLayer.Interface.IProductos;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.Productos
{
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Servicio de manejo de productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("Producto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener productos", Description = "Obtiene una lista de todos los productos o un producto específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id, long idCompany)
        {
            try
            {
                if (id.HasValue)
                {
                    var producto = await _productoService.GetProductById(id.Value);
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
    }
}
