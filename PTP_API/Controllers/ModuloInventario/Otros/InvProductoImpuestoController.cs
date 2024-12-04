using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloInventario.Otros;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloInventario.Otros
{
    [ApiController]
    [SwaggerTag("Gestión de Producto-Impuesto")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class InvProductoImpuestoController : ControllerBase
    {
        private readonly IInvProductoImpuestoService _service;
        private readonly IValidator<CreateInvProductoImpuestoDto> _validatorCreate;
        private readonly IValidator<EditInvProductoImpuestoDto> _validatorEdit;
        private readonly IValidator<long> _validateNumbers;

        public InvProductoImpuestoController(
            IInvProductoImpuestoService service,
            IValidator<CreateInvProductoImpuestoDto> validatorCreate,
            IValidator<EditInvProductoImpuestoDto> validatorEdit,
            IValidator<long> validateNumbers)
        {
            _service = service;
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
        }

        [HttpGet("ObtenerPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

                var data = await _service.GetById(id);
                if (data == null)
                    return NotFound(Response<string>.NotFound("Producto-Impuesto no encontrado."));
                return Ok(Response<ViewInvProductoImpuestoDto>.Success(data, "Producto-Impuesto encontrado."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al obtener el Producto-Impuesto: {ex.Message}"));
            }
        }

        [HttpGet("ObtenerTodos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAll();
                return Ok(Response<List<ViewInvProductoImpuestoDto>>.Success(data, "Listado de Producto-Impuesto."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al obtener el listado: {ex.Message}"));
            }
        }

        [HttpPost("Crear")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(CreateInvProductoImpuestoDto model)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), 400));

                var createdId = await _service.Add(model);
                return Ok(Response<int>.Created(createdId));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al crear el Producto-Impuesto: {ex.Message}"));
            }
        }

        [HttpPut("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(EditInvProductoImpuestoDto model)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(Response<string>.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), 400));

                var existing = await _service.GetById(model.Id);
                if (existing == null)
                    return NotFound(Response<string>.NotFound("Producto-Impuesto no encontrado."));

                await _service.Update(model);
                return Ok(Response<string>.Success("Producto-Impuesto actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al actualizar el Producto-Impuesto: {ex.Message}"));
            }
        }

        [HttpDelete("Eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

                await _service.Delete(id);
                return Ok(Response<string>.Success("Producto-Impuesto eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError($"Error al eliminar el Producto-Impuesto: {ex.Message}"));
            }
        }
    }
}
