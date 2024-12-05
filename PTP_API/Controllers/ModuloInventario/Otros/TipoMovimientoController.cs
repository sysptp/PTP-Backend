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
    [SwaggerTag("Gestión de Tipos de Movimiento")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class TipoMovimientoController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateTipoMovimientoDto> _validatorCreate;
        private readonly IValidator<EditTipoMovimientoDto> _validatorEdit;
        private readonly IValidator<int> _validateNumbers;
        private readonly ITipoMovimientoService _tipoMovimientoService;

        public TipoMovimientoController(
            ITipoMovimientoService tipoMovimientoService,
            IValidator<CreateTipoMovimientoDto> validatorCreate,
            IValidator<EditTipoMovimientoDto> validatorEdit,
            IValidator<int> validateNumbers)
        {
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
            _tipoMovimientoService = tipoMovimientoService;
        }
        #endregion

        #region Endpoints

        [HttpGet("ObtenerPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Tipo de Movimiento", Description = "Obtiene un tipo de movimiento por su ID.")]
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

                var data = await _tipoMovimientoService.GetById(id);

                if (data != null)
                {
                    return Ok(Response<ViewTipoMovimientoDto>.Success(data, "Tipo de movimiento encontrado."));
                }
                else
                {
                    return Ok(Response<ViewTipoMovimientoDto>.NotFound("No se encontró el tipo de movimiento."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener el tipo de movimiento."));
            }
        }

        [HttpGet("ObtenerPorEmpresa/{idEmpresa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Tipos por Empresa", Description = "Obtiene todos los tipos de movimiento de una empresa.")]
        public async Task<IActionResult> GetByCompany(int idEmpresa)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _tipoMovimientoService.GetByCompany(idEmpresa);

                if (data.Count > 0)
                {
                    return Ok(Response<List<ViewTipoMovimientoDto>>.Success(data, "Tipos de movimiento encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewTipoMovimientoDto>>.NotFound("No se encontraron tipos de movimiento para la empresa."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los tipos de movimiento."));
            }
        }

        [HttpPost("Crear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Tipo de Movimiento", Description = "Endpoint para crear un tipo de movimiento.")]
        public async Task<IActionResult> Add(CreateTipoMovimientoDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _tipoMovimientoService.Add(create);

                return Ok(Response<int>.Created(created, "Tipo de movimiento creado exitosamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al crear el tipo de movimiento."));
            }
        }

        [HttpPut("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Editar Tipo de Movimiento", Description = "Endpoint para editar un tipo de movimiento.")]
        public async Task<IActionResult> Edit(EditTipoMovimientoDto edit)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _tipoMovimientoService.GetById(edit.Id);

                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Tipo de movimiento no encontrado."));
                }

                await _tipoMovimientoService.Update(edit);

                return Ok(Response<string>.Success("Tipo de movimiento editado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al editar el tipo de movimiento."));
            }
        }

        [HttpDelete("Eliminar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Tipo de Movimiento", Description = "Endpoint para eliminar un tipo de movimiento.")]
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

                await _tipoMovimientoService.Delete(id);

                return Ok(Response<int>.Success(id, "Tipo de movimiento eliminado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el tipo de movimiento."));
            }
        }

        #endregion
    }
}
