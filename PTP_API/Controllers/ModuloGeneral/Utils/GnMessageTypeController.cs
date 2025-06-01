using BussinessLayer.DTOs.ModuloGeneral.Utils.GnMessageType;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Utils;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Utils
{
    [ApiController]
    [SwaggerTag("Gestión de GnMessageType")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class GnMessageTypeController : ControllerBase
    {
        private readonly IGnMessageTypeService _gnmessagetypeService;

        public GnMessageTypeController(IGnMessageTypeService gnmessagetypeService)
        {
            _gnmessagetypeService = gnmessagetypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnMessageTypeResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener GnMessageType", Description = "Devuelve una lista de GnMessageType o un elemento específico si se proporciona un ID")]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _gnmessagetypeService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<GnMessageTypeResponse>.NotFound("GnMessageType no encontrado."));

                    return Ok(Response<GnMessageTypeResponse>.Success(item, "GnMessageType encontrado."));
                }
                else
                {
                    var items = await _gnmessagetypeService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<GnMessageTypeResponse>>.NoContent("No hay GnMessageType disponibles."));

                    return Ok(Response<IEnumerable<GnMessageTypeResponse>>.Success(items, "GnMessageType obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

    }
}
