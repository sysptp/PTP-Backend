using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Empresas;

namespace PTP_API.Controllers.ModuloGeneral.Empresa
{
    [ApiController]
    [Route("api/v1/Company")]
    [SwaggerTag("Gestión de Empresas")]
    [Authorize]
    [EnableBitacora]
    public class CompanyController : ControllerBase
    {
        private readonly IGnEmpresaservice _empresaService;
        private readonly IValidator<GnEmpresaRequest> _validator;


        public CompanyController(IGnEmpresaservice empresaService, IValidator<GnEmpresaRequest> validator)
        {
            _empresaService = empresaService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<GnEmpresaResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener empresas", Description = "Obtiene una lista de todas las empresas o una empresa específica si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var empresa = await _empresaService.GetByIdResponse(id);
                    if (empresa == null)
                    {
                        return NotFound(Response<GnEmpresaResponse>.NotFound("Empresa no encontrada."));
                    }
                    return Ok(Response<List<GnEmpresaResponse>>.Success(new List<GnEmpresaResponse> { empresa }, "Empresa encontrada."));
                }
                else
                {
                    var empresas = await _empresaService.GetAllDto();
                    if (empresas == null || empresas.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<GnEmpresaResponse>>.Success(empresas, "Empresas obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las empresas. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva empresa", Description = "Crea una nueva empresa en el sistema.")]
        public async Task<IActionResult> Add([FromBody] GnEmpresaRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var empresa = await _empresaService.Add(request);
                return StatusCode(201, Response<GnEmpresaResponse>.Created(empresa, "Empresa creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la empresa. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una empresa", Description = "Actualiza la información de una empresa existente.")]
        public async Task<IActionResult> Update(long id, [FromBody] GnEmpresaRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _empresaService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<GnEmpresaResponse>.NotFound("Empresa no encontrada."));
                }
                saveDto.CODIGO_EMP = id;
                await _empresaService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Empresa actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una empresa", Description = "Elimina una empresa de manera lógica.")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var empresa = await _empresaService.GetByIdResponse(id);
                if (empresa == null)
                {
                    return NotFound(Response<string>.NotFound("Empresa no encontrada."));
                }

                await _empresaService.Delete(id);
                return Ok(Response<string>.Success(null, "Empresa eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la empresa. Por favor, intente nuevamente."));
            }
        }
    }
}
