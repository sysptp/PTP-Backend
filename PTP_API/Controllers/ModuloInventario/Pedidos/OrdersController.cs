using BussinessLayer.FluentValidations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using BussinessLayer.Wrappers;
using BussinessLayer.FluentValidations.ModuloInventario.Pedidos;
using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Marcas;

namespace PTP_API.Controllers.ModuloInventario.Pedidos
{
    [ApiController]
    [SwaggerTag("Gestión de Pedidos")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateOrderDto> _validatorCreate;
        private readonly IValidator<EditOrderDto> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IPedidoService _pedidoService;

        public OrdersController(
            IPedidoService pedidoService,
            IValidator<CreateOrderDto> validationRules,
            IValidator<EditOrderDto> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _pedidoService = pedidoService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerPedido/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Pedido", Description = "Obtiene un pedido en especifico por su id.")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _pedidoService.GetById(id);

                if (data != null)
                {

                    return Ok(Response<ViewOrderDto>.Success(data, "Dato encontrado."));
                }
                else
                {
                    return Ok(Response<ViewOrderDto>.NotFound("No se han encontrado datos."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la data. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerPedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Pedidos", Description = "Obtiene todas los pedidos de una empresa por su id.")]
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

                var data = await _pedidoService.GetByCompany(idCompany);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewOrderDto>>.Success(data, "Datos encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewOrderDto>>.NotFound("No se han encontrado datos."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la data. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearPedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Pedido", Description = "Endpoint para crear pedidos.")]
        public async Task<IActionResult> Add(CreateOrderDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _pedidoService.Create(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el registro. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarPedido")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Pedido", Description = "Endpoint para editar el pedido")]
        public async Task<IActionResult> Update([FromBody] EditOrderDto edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _pedidoService.GetById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Datos no encontrado."));
                }

                await _pedidoService.Edit(edit);


                return Ok(Response<string>.Success("Registro editado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el registro. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarPedidoId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Pedido", Description = "Endpoint para eliminar pedido por id")]
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

                await _pedidoService.DeleteById(id);

                return Ok(Response<int>.Success(id, "Registro eliminado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el registro. Por favor, intente nuevamente."));
            }

        }
    }
}
