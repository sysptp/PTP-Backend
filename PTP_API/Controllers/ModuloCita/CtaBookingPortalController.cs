using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Portal de Reservas Públicas")]
    [Route("api/v1/[controller]")]
    [EnableBitacora]
    public class CtaBookingPortalController : ControllerBase
    {
        private readonly ICtaBookingPortalService _bookingPortalService;

        public CtaBookingPortalController(ICtaBookingPortalService bookingPortalService)
        {
            _bookingPortalService = bookingPortalService;
        }

        // ============================================
        // ENDPOINTS ADMINISTRATIVOS (Requieren autenticación)
        // ============================================

        [HttpPost("admin")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear portal de reservas", Description = "Crea un nuevo portal de reservas para una empresa")]
        public async Task<IActionResult> CreatePortal([FromBody] BookingPortalConfigRequest request)
        {
            try
            {
                var response = await _bookingPortalService.CreatePortalAsync(request);
                return CreatedAtAction(nameof(GetPortalById), new { id = response.Id },
                    Response<BookingPortalConfigResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("admin/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response<BookingPortalConfigResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener portal por ID", Description = "Obtiene la configuración de un portal específico")]
        [DisableBitacora]
        public async Task<IActionResult> GetPortalById(int id)
        {
            try
            {
                var portal = await _bookingPortalService.GetPortalByIdAsync(id);
                if (portal == null)
                    return NotFound(Response<string>.NotFound("Portal no encontrado"));

                return Ok(Response<BookingPortalConfigResponse>.Success(portal, "Portal encontrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("admin/company/{companyId}")]
        [Authorize]
        [ProducesResponseType(typeof(Response<List<BookingPortalConfigResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener portales por empresa", Description = "Obtiene todos los portales de una empresa")]
        [DisableBitacora]
        public async Task<IActionResult> GetPortalsByCompany(long companyId)
        {
            try
            {
                var portals = await _bookingPortalService.GetPortalsByCompanyAsync(companyId);
                if (!portals.Any())
                    return StatusCode(204, Response<string>.NoContent("No hay portales configurados"));

                return Ok(Response<List<BookingPortalConfigResponse>>.Success(portals, "Portales obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("admin/{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar portal", Description = "Actualiza la configuración de un portal existente")]
        public async Task<IActionResult> UpdatePortal(int id, [FromBody] BookingPortalConfigRequest request)
        {
            try
            {
                var response = await _bookingPortalService.UpdatePortalAsync(id, request);
                return Ok(Response<BookingPortalConfigResponse>.Success(response, "Portal actualizado exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("admin/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar portal", Description = "Elimina un portal de reservas")]
        public async Task<IActionResult> DeletePortal(int id)
        {
            try
            {
                await _bookingPortalService.DeletePortalAsync(id);
                return Ok(Response<string>.Success(null, "Portal eliminado exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        // ============================================
        // ENDPOINTS PÚBLICOS (Sin autenticación JWT)
        // ============================================

        [HttpGet("public/portal/{slug}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<BookingPortalConfigResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Obtener portal público", Description = "Obtiene la información pública de un portal de reservas")]
        [DisableBitacora]
        public async Task<IActionResult> GetPublicPortal(string slug)
        {
            try
            {
                var portal = await _bookingPortalService.GetPortalBySlugAsync(slug);
                if (portal == null)
                    return NotFound(Response<string>.NotFound("Portal no encontrado"));

                return Ok(Response<BookingPortalConfigResponse>.Success(portal, "Portal encontrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("public/authenticate")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<ClientAuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Autenticar cliente", Description = "Autentica un cliente usando su número de teléfono")]
        public async Task<IActionResult> AuthenticateClient([FromBody] ClientAuthenticationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.PhoneNumber) || string.IsNullOrEmpty(request.PortalSlug))
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Número de teléfono y slug del portal son requeridos" }, 400));
                }

                var response = await _bookingPortalService.AuthenticateClientAsync(request);
                return Ok(Response<ClientAuthenticationResponse>.Success(response,
                    response.IsAuthenticated ? "Autenticación exitosa" : "Cliente no registrado"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("public/available-slots")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<AvailableSlotResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Obtener horarios disponibles", Description = "Obtiene los horarios disponibles para una fecha específica")]
        [DisableBitacora]
        public async Task<IActionResult> GetAvailableSlots([FromBody] AvailableSlotRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.PortalSlug))
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Slug del portal es requerido" }, 400));
                }

                var response = await _bookingPortalService.GetAvailableSlotsAsync(request);
                return Ok(Response<AvailableSlotResponse>.Success(response, "Horarios obtenidos exitosamente"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(Response<string>.Unauthorized(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("public/appointment")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<PublicAppointmentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Crear cita pública", Description = "Crea una nueva cita desde el portal público")]
        public async Task<IActionResult> CreatePublicAppointment([FromBody] PublicAppointmentRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.PortalSlug))
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Slug del portal es requerido" }, 400));
                }

                var response = await _bookingPortalService.CreatePublicAppointmentAsync(request);

                if (!response.Success)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { response.Message }, 400));
                }

                return CreatedAtAction(nameof(GetPublicPortal), new { slug = request.PortalSlug },
                    Response<PublicAppointmentResponse>.Created(response));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(Response<string>.Unauthorized(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("admin/generate-slug")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Generar slug único", Description = "Genera un slug único basado en un nombre")]
        [DisableBitacora]
        public async Task<IActionResult> GenerateSlug([FromBody] GenerateSlugRequest request)
        {
            try
            {
                var slug = await _bookingPortalService.GenerateUniqueSlugAsync(request.BaseName);
                return Ok(Response<string>.Success(slug, "Slug generado exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }

    public class GenerateSlugRequest
    {
        public string BaseName { get; set; } = null!;
    }
}