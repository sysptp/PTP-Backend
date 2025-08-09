using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Email
{
    [ApiController]
    [SwaggerTag("Envío de correos")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class GnEmailController : ControllerBase
    {
        private readonly IGnEmailService _emailService;
        private readonly IValidator<GnEmailMessageDto> _validator;

        public GnEmailController(IGnEmailService emailService, IValidator<GnEmailMessageDto> validator)
        {
            _emailService = emailService;
            _validator = validator;
        }

        /// <summary>
        /// Envía un correo electrónico (permite adjuntos).
        /// </summary>
        [HttpPost("send")]
        [Consumes(MediaTypeNames.Multipart.FormData)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Enviar correo", Description = "Envía un correo usando la configuración SMTP de la empresa.")]
        public async Task<IActionResult> SendEmail([FromForm] GnEmailMessageDto request)
        {
            try
            {
                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _emailService.SendAsync(request, request.CompanyId);
                return Ok(Response<string>.Success(null, "Correo enviado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("send-json")]
        [Consumes("application/json")]
       
        public async Task<IActionResult> SendEmailJson([FromBody] GnEmailMessageDto request)
        {
            var validation = await _validator.ValidateAsync(request);
            if (!validation.IsValid)
                return BadRequest(Response<string>.BadRequest(validation.Errors.Select(e => e.ErrorMessage).ToList(), 400));

            await _emailService.SendAsync(request, request.CompanyId);
            return Ok(Response<string>.Success(null, "Correo enviado correctamente."));
        }

    }
}
