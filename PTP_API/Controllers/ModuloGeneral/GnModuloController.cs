using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Modulo;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral
{
    [ApiController]
    [SwaggerTag("Gestión de Tipo de Participantes")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class GnModuloController : ControllerBase
    {
        private readonly IGnModuloService _moduloService;

        public GnModuloController(IGnModuloService moduloService)
        {
            _moduloService = moduloService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnModuloResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Los módulo del sistema", Description = "Devuelve una lista de módulos del sistema")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllModules([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var module = await _moduloService.GetByIdResponse(id.Value);
                    if (module == null)
                        return NotFound(Response<GnModuloResponse>.NotFound("Módulo no encontrada."));

                    return Ok(Response<GnModuloResponse>.Success(module, "Módulo encontrada."));
                }
                else
                {
                    var modules = await _moduloService.GetAllDto();
                    if (modules == null || !modules.Any())
                        return StatusCode(204, Response<IEnumerable<GnModuloResponse>>.NoContent("No hay Módulos disponibles."));

                    return Ok(Response<IEnumerable<GnModuloResponse>>.Success(modules, "Módulos obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
