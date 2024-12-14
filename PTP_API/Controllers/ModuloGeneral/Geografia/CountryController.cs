using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.DTOs.Configuracion.Geografia.DPais;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais;
using BussinessLayer.Interfaces.Language;
using BussinessLayer.Services.Language.Translation;
using DataLayer.Models.ModuloInventario.Productos;

namespace PTP_API.Controllers.ModuloGeneral.Geografia
{
    [ApiController]
    [Route("api/v1/Country")]
    [SwaggerTag("Gestión de Países")]
    [Authorize]
    [EnableAuditing]
    public class CountryController : ControllerBase
    {
        private readonly IPaisService _countryService;
        private readonly IValidator<CountryRequest> _validator;
        private readonly IJsonTranslationService _jsonTranslationService;

        public CountryController(IPaisService countryService, IValidator<CountryRequest> validator, IJsonTranslationService jsonTranslationService)
        {
            _countryService = countryService;
            _validator = validator;
            _jsonTranslationService = jsonTranslationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener países", Description = "Obtiene una lista de todos los países o un país específico si se proporciona un ID.")]
        [DisableAuditing]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var country = await _countryService.GetByIdResponse((int)id);
                    if (country == null)
                    {
                        return NotFound(Response<CountryResponse>.NotFound("País no encontrado."));
                    }
                    return Ok(Response<CountryResponse>.Success(country, "País encontrado."));
                }
                else
                {
                    var countries = await _countryService.GetAllDto();
                    if (countries == null || !countries.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<CountryResponse>>.NoContent("No hay países disponibles."));
                    }

                    var translatedCountry = await _jsonTranslationService.TranslateEntities(countries);
                    return Ok(Response<IEnumerable<CountryResponse>>.Success(countries, "Países obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear un nuevo país", Description = "Crea un nuevo país en el sistema.")]
        public async Task<IActionResult> Add([FromBody] CountryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var country = await _countryService.Add(request);
                return Ok(Response<CountryResponse>.Created(country, "País creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar país", Description = "Endpoint para actualizar los datos de un país")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingCountry = await _countryService.GetByIdRequest(id);
                if (existingCountry == null)
                    return NotFound(Response<string>.NotFound("País no encontrado"));

                request.Id = id;
                await _countryService.Update(request, id);
                return Ok(Response<string>.Success(null, "País actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar país", Description = "Endpoint para eliminar un país")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var existingCountry = await _countryService.GetByIdRequest(id);
                if (existingCountry == null)
                    return NotFound(Response<string>.NotFound("País no encontrado"));

                await _countryService.Delete(id);
                return Ok(Response<string>.Success(null, "País eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

    }
}
