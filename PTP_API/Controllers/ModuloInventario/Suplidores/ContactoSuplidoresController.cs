using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.Services.ModuloInventario.Suplidores;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloInventario.Suplidores
{
    [ApiController]
    [SwaggerTag("Gestión de Contactos Suplidores")]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ContactoSuplidoresController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateContactosSuplidoresDto> _validatorCreate;
        private readonly IValidator<EditContactosSuplidoresDto> _validatorEdit;
        private readonly IValidator<int> _validateNumbers;
        private readonly IContactosSuplidoresService _contactosSuplidoresService;

        public ContactoSuplidoresController(
            IContactosSuplidoresService contactosSuplidoresService,
            IValidator<CreateContactosSuplidoresDto> validatorCreate,
            IValidator<EditContactosSuplidoresDto> validatorEdit,
            IValidator<int> validateNumbers)
        {
            _validatorCreate = validatorCreate;
            _validatorEdit = validatorEdit;
            _validateNumbers = validateNumbers;
            _contactosSuplidoresService = contactosSuplidoresService;
        }
        #endregion

        #region Endpoints

        [HttpGet("ObtenerContacto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Contacto", Description = "Obtiene un contacto de suplidor específico por su id.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _contactosSuplidoresService.GetById(id);

                if (data != null)
                {
                    return Ok(Response<ViewContactosSuplidoresDto>.Success(data, "Contacto encontrado."));
                }
                else
                {
                    return Ok(Response<ViewContactosSuplidoresDto>.NotFound("No se encontró el contacto."));
                }
            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener el contacto. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("ObtenerContactosPorEmpresa/{idEmpresa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Contactos por Empresa", Description = "Obtiene todos los contactos de suplidores para una empresa específica.")]
        public async Task<IActionResult> GetByCompany(int idEmpresa)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idEmpresa);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _contactosSuplidoresService.GetByCompany(idEmpresa);

                if (data.Count > 0)
                {
                    return Ok(Response<List<ViewContactosSuplidoresDto>>.Success(data, "Contactos encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewContactosSuplidoresDto>>.NotFound("No se encontraron contactos para la empresa."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los contactos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("CrearContacto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Contacto", Description = "Endpoint para crear un contacto de suplidor.")]
        public async Task<IActionResult> Add(CreateContactosSuplidoresDto create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _contactosSuplidoresService.Add(create);

                return Ok(Response<int>.Created(created, "Contacto creado exitosamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al crear el contacto. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("EditarContacto")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Contacto", Description = "Endpoint para editar un contacto de suplidor.")]
        public async Task<IActionResult> Edit(EditContactosSuplidoresDto edit)
        {
            try
            {
                var validationResult = await _validatorEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _contactosSuplidoresService.GetById(edit.Id);

                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Contacto no encontrado."));
                }

                await _contactosSuplidoresService.Update(edit);

                return Ok(Response<string>.Success("Contacto editado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al editar el contacto. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("EliminarContacto/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Contacto", Description = "Endpoint para eliminar un contacto por su id.")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(id);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                await _contactosSuplidoresService.Delete(id);

                return Ok(Response<int>.Success(id, "Contacto eliminado correctamente."));
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el contacto. Por favor, intente nuevamente."));
            }
        }
        #endregion
    }

}
