//using Microsoft.AspNetCore.Mvc;
//using System.Net.Mime;
//using Swashbuckle.AspNetCore.Annotations;
//using BussinessLayer.DTOs.Empresas;
//using BussinessLayer.Interfaces.IEmpresa;
//using BussinessLayer.Wrappers;

//namespace PTP_API.Controllers.Empresa
//{
//    [ApiController]
//    [Route("api/v1/Empresa")]
//    [SwaggerTag("Gestión de Empresas")]
//    public class SC_EMP001Controller : ControllerBase
//    {
//        private readonly ISC_EMP001service _scEmp001Service;

//        public SC_EMP001Controller(ISC_EMP001service scEmp001Service)
//        {
//            _scEmp001Service = scEmp001Service;
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [SwaggerOperation(Summary = "Obtener empresas", Description = "Obtiene una lista de todas las empresas o una empresa específica si se proporciona un ID.")]
//        public async Task<IActionResult> Get([FromQuery] long? id)
//        {
//            try
//            {
//                if (id.HasValue)
//                {
//                    var empresa = await _scEmp001Service.GetByCodEmp(id.Value);
//                    if (empresa == null)
//                    {
//                        return NotFound(Response<SC_EMP001Dto>.NotFound("Empresa no encontrada."));
//                    }
//                    return Ok(Response<SC_EMP001Dto>.Success(empresa, "Empresa encontrada."));
//                }
//                else
//                {
//                    var empresas = await _scEmp001Service.GetAllDto();
//                    if (empresas == null || empresas.Count == 0)
//                    {
//                        return Ok(Response<IEnumerable<SC_EMP001Dto>>.NoContent("No hay empresas disponibles."));
//                    }
//                    return Ok(Response<IEnumerable<SC_EMP001Dto>>.Success(empresas, "Empresas obtenidas correctamente."));
//                }
//            }
//            catch (Exception ex)
//            {
//                return Ok(Response<string>.ServerError("Ocurrió un error al obtener las empresas. Por favor, intente nuevamente."));
//            }
//        }

//        [HttpPost]
//        [Consumes(MediaTypeNames.Application.Json)]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [SwaggerOperation(Summary = "Crear una nueva empresa", Description = "Crea una nueva empresa en el sistema.")]
//        public async Task<IActionResult> Add([FromBody] SaveSC_EMP001Dto saveDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
//                return BadRequest(Response<string>.BadRequest(errors, 400));
//            }

//            try
//            {
//                var empresa = await _scEmp001Service.Add(saveDto);
//                return Ok(Response<SaveSC_EMP001Dto>.Created(empresa, "Empresa creada correctamente."));
//            }
//            catch (Exception ex)
//            {
//                return Ok(Response<string>.ServerError("Ocurrió un error al crear la empresa. Por favor, intente nuevamente."));
//            }
//        }

//        [HttpPut("{id}")]
//        [Consumes(MediaTypeNames.Application.Json)]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [SwaggerOperation(Summary = "Actualizar una empresa", Description = "Actualiza la información de una empresa existente.")]
//        public async Task<IActionResult> Update(int id, [FromBody] SaveSC_EMP001Dto saveDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
//                return BadRequest(Response<string>.BadRequest(errors, 400));
//            }

//            try
//            {
//                var existingEmpresa = await _scEmp001Service.GetByIdSaveDto(id);
//                if (existingEmpresa == null)
//                {
//                    return NotFound(Response<SC_EMP001Dto>.NotFound("Empresa no encontrada."));
//                }

//                await _scEmp001Service.Update(saveDto, id);
//                return Ok(Response<string>.Success("Empresa actualizada correctamente."));
//            }
//            catch (Exception ex)
//            {
//                return Ok(Response<string>.ServerError("Ocurrió un error al actualizar la empresa. Por favor, intente nuevamente."));
//            }
//        }

//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [SwaggerOperation(Summary = "Eliminar una empresa", Description = "Elimina una empresa de manera lógica.")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                var empresa = await _scEmp001Service.GetByIdSaveDto(id);
//                if (empresa == null)
//                {
//                    return NotFound(Response<string>.NotFound("Empresa no encontrada."));
//                }

//                await _scEmp001Service.Delete(id);
//                return Ok(Response<string>.Success("Empresa eliminada correctamente."));
//            }
//            catch (Exception ex)
//            {
//                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar la empresa. Por favor, intente nuevamente."));
//            }
//        }
//    }
//}
