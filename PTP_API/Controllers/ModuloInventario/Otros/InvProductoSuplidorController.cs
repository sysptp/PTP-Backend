using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.Services.ModuloInventario.Otros;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloInventario.Otros
{
    [ApiController]
    [SwaggerTag("Gestión de ProductosSuplidores")]
    [Authorize]
    public class InvProductoSuplidorController : ControllerBase
    {
        #region Propiedades
        private readonly IValidator<CreateInvProductoSuplidorDTO> _validatorCreate;
        private readonly IValidator<EditInvProductoSuplidorDTO> _validationsEdit;
        private readonly IValidator<long> _validateNumbers;
        private readonly IValidator<string> _validateString;
        private readonly IInvProductoSuplidorService _invProductoSuplidorService;

        public InvProductoSuplidorController(
            IInvProductoSuplidorService invProductoSuplidorService,
            IValidator<CreateInvProductoSuplidorDTO> validationRules,
            IValidator<EditInvProductoSuplidorDTO> validations,
            IValidator<string> validateString,
            IValidator<long> validateNumbers)
        {
            _validatorCreate = validationRules;
            _validationsEdit = validations;
            _validateString = validateString;
            _validateNumbers = validateNumbers;
            _invProductoSuplidorService = invProductoSuplidorService;
        }
        #endregion

        [HttpGet("api/v1/[controller]/ObtenerProdSupli")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener Producto Suplidor", Description = "Obtiene un producto suplidor en especifico por su id.")]
        public async Task<IActionResult> GetAllByFilters(int? id, long? idCompany)
        {
            try
            {
                if (id.HasValue)
                {
                    var productoSuplidor = await _invProductoSuplidorService.GetById((int)id);
                    if (productoSuplidor != null)
                    {
                        return Ok(Response<List<ViewInvProductoSuplidorDTO>>.Success(new List<ViewInvProductoSuplidorDTO> { productoSuplidor }, "Registro encontrado."));
                    }
                    else
                    {
                        return Ok(Response<ViewInvProductoSuplidorDTO>.NotFound("No se han encontrado Registros."));
                    }
                }
                var productoSuplidores = await _invProductoSuplidorService.GetAll();

                if (productoSuplidores == null || productoSuplidores.Count == 0)
                {
                    return Ok(Response<List<ViewInvProductoSuplidorDTO>>.NoContent("No se econtraron productos suplidores"));
                }

                return Ok(Response<List<ViewInvProductoSuplidorDTO>>.Success(
                    idCompany != null ?
                    productoSuplidores.Where(x => x.IdEmpresa == idCompany).ToList() : productoSuplidores, "Registro encontrado."));


            }
            catch (Exception)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener la moneda. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerPorProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener por Producto", Description = "Obtiene todas los registros por producto.")]
        public async Task<IActionResult> GetByProduct([FromQuery] int idProduct)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idProduct);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _invProductoSuplidorService.GetAllByProduct(idProduct);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewInvProductoSuplidorDTO>>.Success(data, "Registros encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewInvProductoSuplidorDTO>>.NotFound("No se han encontrado registros."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los registros. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("api/v1/[controller]/ObtenerPorSuplidor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener por Suplidor", Description = "Obtiene todas los registros por suplidor.")]
        public async Task<IActionResult> GetBySupplier([FromQuery] int idSupplier)
        {
            try
            {
                var validationResult = await _validateNumbers.ValidateAsync(idSupplier);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var data = await _invProductoSuplidorService.GetAllBySupplier(idSupplier);

                if (data.Count > 0)
                {

                    return Ok(Response<List<ViewInvProductoSuplidorDTO>>.Success(data, "Registros encontrados."));
                }
                else
                {
                    return Ok(Response<List<ViewInvProductoSuplidorDTO>>.NotFound("No se han encontrado registros."));
                }
            }
            catch
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los registros. Por favor, intente nuevamente."));
            }
        }

        [HttpPost("api/v1/[controller]/CrearProdSupli")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear Producto Suplidor", Description = "Endpoint para crear producto suplidor.")]
        public async Task<IActionResult> Add(CreateInvProductoSuplidorDTO create)
        {
            try
            {
                var validationResult = await _validatorCreate.ValidateAsync(create);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var created = await _invProductoSuplidorService.Add(create);

                return Ok(Response<int?>.Created(created));

            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al crear el Registro. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("api/v1/[controller]/EditarProdSupli")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Editar Producto Suplidor", Description = "Endpoint para editar producto suplidor")]
        public async Task<IActionResult> Edit([FromBody] EditInvProductoSuplidorDTO edit)
        {
            try
            {
                var validationResult = await _validationsEdit.ValidateAsync(edit);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existing = await _invProductoSuplidorService.GetById(edit.Id);
                if (existing == null)
                {
                    return NotFound(Response<string>.NotFound("Registro no encontrado."));
                }

                await _invProductoSuplidorService.Update(edit);


                return Ok(Response<string>.Success("Registro editado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al editar el registro. Por favor, intente nuevamente."));
            }

        }

        [HttpDelete("api/v1/[controller]/EliminarProdSupliId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Eliminar Producto Suplidor", Description = "Endpoint para eliminar producto suplidor por id")]
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

                await _invProductoSuplidorService.Delete(id);

                return Ok(Response<int>.Success(id, "Registro eliminado correctamente"));
            }
            catch
            {

                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el registro. Por favor, intente nuevamente."));
            }

        }
    }
}
