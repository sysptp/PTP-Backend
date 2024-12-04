using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.Interfaces.ModuloInventario.Pedidos;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloInventario.Pedidos
{
    [ApiController]
    [SwaggerTag("Gestión de Detalles de Pedido")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateDetallePedidoDto> _validatorCreate;
        private readonly IValidator<EditDetallePedidoDto> _validatorEdit;
        private readonly IValidator<int> _validateNumbers;
        private readonly IDetallePedidoService _detallePedidoService;

        public DetallePedidoController(
            IDetallePedidoService detallePedidoService,
            IValidator<CreateDetallePedidoDto> validatorCreate,
            IValidator<EditDetallePedidoDto> validatorEdit,
            IValidator<int> validateNumbers)
        {
            _detallePedidoService = detallePedidoService;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }
        #endregion

        [HttpGet("ObtenerDetallePedido/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Detalle de Pedido", Description = "Obtiene un detalle de pedido específico por su ID.")]
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

                var data = await _detallePedidoService.GetById(id);
                if (data != null)
                {
                    return Ok(Response<ViewDetallePedidoDto>.Success(data, "Detalle de pedido encontrado."));
                }
                else
                {
                    return Ok(Response<ViewDetallePedidoDto>.NotFound("No se encontró el detalle de pedido."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener el detalle de pedido. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("ObtenerDetallesPedidos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Detalles de Pedido", Description = "Obtiene todos los detalles de pedido de una empresa por su ID.")]
        public async Task<IActionResult> GetByCompany([FromQuery] long idEmpresa)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync((int)idEmpresa);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _detallePedidoService.GetByCompany(idEmpresa);
                if (data.Count > 0)
                {
                    return Ok(Response<List<ViewDetallePedidoDto>>.Success(data, "Detalles de pedido encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewDetallePedidoDto>>.NotFound("No se encontraron detalles de pedido."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los detalles de pedido. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("CrearDetallePedido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Detalle de Pedido", Description = "Endpoint para crear un detalle de pedido.")]
        public async Task<IActionResult> Add(CreateDetallePedidoDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _detallePedidoService.Add(create);
                return Ok(Response<int?>.Created(created));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al crear el detalle de pedido. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("EditarDetallePedido")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Detalle de Pedido", Description = "Endpoint para editar un detalle de pedido.")]
        public async Task<IActionResult> Edit([FromBody] EditDetallePedidoDto edit)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(edit);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _detallePedidoService.GetById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Detalle de pedido no encontrado."));
                }

                await _detallePedidoService.Update(edit);
                return Ok(Response<string>.Success("Detalle de pedido editado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al editar el detalle de pedido. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("EliminarDetallePedido/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Detalle de Pedido", Description = "Endpoint para eliminar un detalle de pedido por su ID.")]
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

                await _detallePedidoService.Delete(id);
                return Ok(Response<int>.Success(id, "Detalle de pedido eliminado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el detalle de pedido. Por favor, intente nuevamente."));
            }
        }
    }

}
