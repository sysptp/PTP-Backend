using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.Seguridad
{
    [ApiController]
    [Route("api/v{version:apiVersion}/perfil")]
    [ApiVersion("1.0")]
    [SwaggerTag("Servicio de manejo de perfiles")]
    public class PerfilController : ControllerBase
    {
        private readonly IGnPerfilService _gnPerfilService;

        public PerfilController(IGnPerfilService gnPerfilService)
        {
            _gnPerfilService = gnPerfilService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnPerfilDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Obtener Información de los Perfiles")]
        public async Task<ActionResult> GetAll([FromQuery] int? idPerfil, [FromQuery] long? idEmpresa)
        {
            var response = await _gnPerfilService.GetAll(idPerfil, idEmpresa);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<GnPerfilDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear perfil")]
        public async Task<ActionResult> Add([FromBody] SaveGnPerfilDto dto)
        {
            var response = await _gnPerfilService.Add(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PatchUpdate(int id, [FromBody] Dictionary<string, object> updatedProperties)
        {
            var response = await _gnPerfilService.PatchUpdate(id, updatedProperties);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _gnPerfilService.Delete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
