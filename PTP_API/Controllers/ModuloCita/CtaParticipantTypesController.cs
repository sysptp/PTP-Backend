using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaParticipantTypes;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Tipo de Participantes")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaParticipantTypesController : ControllerBase
    {
        private readonly ICtaParticipantTypesServices _participantsTypesServices;

        public CtaParticipantTypesController(ICtaParticipantTypesServices participantsTypesServices)
        {
            _participantsTypesServices = participantsTypesServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaParticipantTypesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Tipo de Participantes", Description = "Devuelve una lista de Tipos de Participantes o un participante en específico")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllNotificationSettings([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var participantType = await _participantsTypesServices.GetByIdResponse(id.Value);
                    if (participantType == null)
                        return NotFound(Response<CtaParticipantTypesResponse>.NotFound("Configuración no encontrada."));

                    return Ok(Response<CtaParticipantTypesResponse>.Success(participantType, "Configuración encontrada."));
                }
                else
                {
                    var participantTypes = await _participantsTypesServices.GetAllDto();
                    if (participantTypes == null || !participantTypes.Any())
                        return StatusCode(204, Response<IEnumerable<CtaParticipantTypesResponse>>.NoContent("No hay configuraciones disponibles."));

                    return Ok(Response<IEnumerable<CtaParticipantTypesResponse>>.Success(participantTypes, "Configuraciones obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
