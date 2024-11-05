using Azure.Core;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.DTOs.ModuloInventario;
using BussinessLayer.FluentValidations.Account;
using BussinessLayer.FluentValidations.Productos;
using BussinessLayer.Interfaces.ModuloInventario;
using BussinessLayer.Wrappers;
using DataLayer.Models.Empresa;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener productos", Description = "Obtiene una lista de todos los productos " +
            "dentro de una empresa o un producto específico de una empresasi se proporciona un codigo de producto.")]
        public async Task<IActionResult> Get([FromQuery] string? codeProduct, long idCompany)
        {
            try
            {
                if (idCompany == 0)
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


        [HttpPost]
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
    }
}
