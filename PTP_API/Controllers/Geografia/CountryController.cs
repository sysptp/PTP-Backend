using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.Geografia.DPais;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IGeografia;

namespace PTP_API.Controllers.Geografia
{
    [ApiController]
    [Route("api/v1/Country")]
    [SwaggerTag("Gestión de Países")]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IPaisService _countryService;

        public CountryController(IPaisService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener países", Description = "Obtiene una lista de todos los países o un país específico si se proporciona un ID.")]
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
                    return Ok(Response<IEnumerable<CountryResponse>>.Success(countries, "Países obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener los países. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear un nuevo país", Description = "Crea un nuevo país en el sistema.")]
        public async Task<IActionResult> Add([FromBody] CountryRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var country = await _countryService.Add(request);
                return Ok(Response<CountryResponse>.Created(country, "País creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el país. Por favor, intente nuevamente."));
            }
        }
    }
}
