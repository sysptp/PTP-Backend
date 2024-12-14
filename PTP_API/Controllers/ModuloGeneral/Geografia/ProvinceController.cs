using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IGeografia;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DProvincia;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia;

namespace PTP_API.Controllers.ModuloGeneral.Geografia
{
    [ApiController]
    [Route("api/v1/Province")]
    [SwaggerTag("Gestión de Provincias")]
    [Authorize]
    [EnableAuditing]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinciaService _provinceService;
        private readonly IValidator<ProvinceRequest> _validator;

        public ProvinceController(IProvinciaService provinceService, IValidator<ProvinceRequest> validator)
        {
            _provinceService = provinceService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener provincias", Description = "Obtiene una lista de todas las provincias o una provincia específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id, int? regionId)
        {
            try
            {
                if (id.HasValue)
                {
                    var province = await _provinceService.GetByIdResponse((int)id);
                    if (province == null)
                    {
                        return NotFound(Response<ProvinceResponse>.NotFound("Provincia no encontrada."));
                    }
                    return Ok(Response<ProvinceResponse>.Success(province
                        , "Provincia encontrada."));
                }
                else
                {
                    var provinces = await _provinceService.GetAllDto();
                    if (provinces == null || !provinces.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<ProvinceResponse>>.NoContent("No hay provincias disponibles."));
                    }
                    return Ok(Response<IEnumerable<ProvinceResponse>>.Success(regionId == null ? provinces : provinces.Where(x => x.RegionId == regionId), "Provincias obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las provincias. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear una nueva provincia", Description = "Crea una nueva provincia en el sistema.")]
        public async Task<IActionResult> Add([FromBody] ProvinceRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var province = await _provinceService.Add(request);
                return Ok(Response<ProvinceResponse>.Created(province, "Provincia creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la provincia. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar provincia", Description = "Endpoint para actualizar los datos de una provincia")]
        public async Task<IActionResult> UpdateProvince(int id, [FromBody] ProvinceRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingProvince = await _provinceService.GetByIdRequest(id);
                if (existingProvince == null)
                    return NotFound(Response<string>.NotFound("Provincia no encontrada"));

                request.Id = id;
                await _provinceService.Update(request, id);
                return Ok(Response<string>.Success(null, "Provincia actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la provincia. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar provincia", Description = "Endpoint para eliminar una provincia")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            try
            {
                var existingProvince = await _provinceService.GetByIdRequest(id);
                if (existingProvince == null)
                    return NotFound(Response<string>.NotFound("Provincia no encontrada"));

                await _provinceService.Delete(id);
                return Ok(Response<string>.Success(null, "Provincia eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la provincia. Por favor, intente nuevamente."));
            }
        }

    }
}
