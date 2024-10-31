//using AutoMapper;
//using BussinessLayer.DTOs.Seguridad;
//using BussinessLayer.Interfaces.ISeguridad;
//using DataLayer.Models.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Annotations;
//using System.Net.Mime;


//namespace PTP_API.Controllers.Seguridad
//{

//    [ApiController]
//    [ApiVersion("1.0")]
//    [SwaggerTag("Servicio de manejo de perfiles")]
//    public class PerfilController : Controller
//    {
//        private readonly IGnPerfilService _gnPerfilService;
//        private readonly IMapper _mapper;

//        public PerfilController(IGnPerfilService gnPerfilService, IMapper mapper)
//        {
//            _gnPerfilService = gnPerfilService;
//            _mapper = mapper;
//        }

//        [HttpGet("Perfil")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        [Consumes(MediaTypeNames.Application.Json)]
//        [SwaggerOperation(
//        Summary = "Obtener Información de los Perfiles",
//        Description = "Obtenemos todas los perfiles, los perfiles por empresa y perfil por Id"
//      )]
//        public async Task<ActionResult<IEnumerable<GnPerfilDto>>> GetAll([FromQuery] int? idPerfil, [FromQuery] long? idEmpresa)
//        {
//            var perfiles = await _gnPerfilService.GetAll(idPerfil, idEmpresa);
//            if (perfiles == null || perfiles.Count == 0)
//            {
//                return NoContent();
//            }
//            if (perfiles == null || perfiles.Count == 0)
//            {
//                return NotFound("No se encontraron perfiles con los parámetros proporcionados.");
//            }
//            return Ok(perfiles);
//        }
        
//        [HttpPost("Perfil")]
//        [Consumes(MediaTypeNames.Application.Json)]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [SwaggerOperation(
//        Summary = "Crear perfil",
//        Description = @"Se utiliza este endpoint para que las empresa pueda registrar el los perfiles de sus 
//                usuarios"

//      )]
//        public async Task<ActionResult> Add([FromBody] GnPerfilDto dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var perfil = _mapper.Map<GnPerfil>(dto);
//            await _gnPerfilService.Add(perfil);
//            var createdPerfilDto = _mapper.Map<GnPerfilDto>(perfil);

//            return NoContent();
//        }

//        [HttpPatch("{id}")]
//        public async Task<ActionResult> PatchUpdate(int id, [FromBody] Dictionary<string, object> updatedProperties)
//        {
//            if (updatedProperties == null || !updatedProperties.Any())
//                return BadRequest("No se proporcionaron propiedades para actualizar.");

//            try
//            {
//                await _gnPerfilService.PatchUpdate(id, updatedProperties);
//                return NoContent();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                return NotFound(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(int id)
//        {
//            var perfil = await _gnPerfilService.GetById(id);
//            if (perfil == null) return NotFound("Perfil no encontrado.");

//            await _gnPerfilService.Delete(id);
//            return NoContent();
//        }

//    }

//}
