using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;

namespace PTP_API.Controllers.ModuloAuditoria
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Login")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IAleLoginService _AleLoginService;
        private readonly IValidator<AleLoginRequest> _validator;

        public LoginController(IAleLoginService AleLoginService, IValidator<AleLoginRequest> validator)
        {
            _AleLoginService = AleLoginService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleLoginReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Auditoria del Login", Description = "Obtiene una lista de todas las Auditoria del Login o una Auditoria del Login específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var AleLogin = await _AleLoginService.GetByIdResponse(id);
                    if (AleLogin == null)
                    {
                        return NotFound(Response<AleLoginReponse>.NotFound("Auditoria del Login no encontrada."));
                    }
                    return Ok(Response<List<AleLoginReponse>>.Success(new List<AleLoginReponse> { AleLogin }, "Auditoria del Login encontrada."));
                }
                else
                {
                    var AleLogins = await _AleLoginService.GetAllDto();
                    if (AleLogins == null || AleLogins.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleLoginReponse>>.Success(AleLogins, "Auditoria del Login obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las Auditoria del Logins. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva Auditoria del Login", Description = "Crea una nueva Auditoria del Login en el sistema.")]
        public async Task<IActionResult> Add([FromBody] AleLoginRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var AleLogin = await _AleLoginService.Add(request);
                return StatusCode(201, Response<AleLoginReponse>.Created(AleLogin, "Auditoria del Login creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Auditoria del Login. Por favor, intente nuevamente."));
            }
        }

    }
}
