using BussinessLayer.DTOs.ModuloGeneral.Imagenes;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.Interfaces.ModuloGeneral.Imagenes;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Imagenes
{
    [ApiController]
    [SwaggerTag("Gestión de Imagenes")]
    [Authorize]
    public class ImageController : ControllerBase
    {
        //#region Propiedades
        //private readonly IValidator<AddImageProductDTO> _validatorCreate;
        //private readonly IValidator<long> _validateNumbers;
        //private readonly IValidator<string> _validateString;
        //private readonly IImagenesService _imagenesService;

        //public ImageController(
        //    IImagenesService imagenesService,
        //    IValidator<AddImageProductDTO> validationRules,
        //    IValidator<string> validateString,
        //    IValidator<long> validateNumbers)
        //{
        //    _validatorCreate = validationRules;
        //    _validateString = validateString;
        //    _validateNumbers = validateNumbers;
        //    _imagenesService = imagenesService;
        //}
        //#endregion

        //[HttpPost("api/v1/[controller]/AgregarImagen")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerOperation(Summary = "Agregar Imagen", Description = "Endpoint para agregar imagen.")]
        //public async Task<IActionResult> Add(AddImageProductDTO create)
        //{
        //    try
        //    {
        //        var validationResult = await _validatorCreate.ValidateAsync(create);

        //        if (!validationResult.IsValid)
        //        {
        //            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        //            return BadRequest(Response<string>.BadRequest(errors, 400));
        //        }

        //        var created = await _imagenesService.Add(create);

        //        return Ok(Response<int?>.Created(created));

        //    }
        //    catch
        //    {

        //        return Ok(Response<string>.ServerError("Ocurrió un error al agregar la imagen. Por favor, intente nuevamente."));
        //    }
        //}

        //[HttpDelete("api/v1/[controller]/EliminarImagenId/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerOperation(Summary = "Eliminar imagen", Description = "Endpoint para eliminar imagen por id")]
        //public async Task<IActionResult> DeleteById(int id)
        //{
        //    try
        //    {
        //        var validationResult = await _validateNumbers.ValidateAsync(id);

        //        if (!validationResult.IsValid)
        //        {
        //            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        //            return BadRequest(Response<string>.BadRequest(errors, 400));
        //        }

        //        await _imagenesService.Delete(id);

        //        return Ok(Response<int>.Success(id, "Imagen eliminada correctamente"));
        //    }
        //    catch
        //    {

        //        return Ok(Response<string>.ServerError("Ocurrió un error al eliminar la imagen. Por favor, intente nuevamente."));
        //    }

        //}

        //[HttpPost("api/v1/[controller]/AgregarImagenes")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[SwaggerOperation(Summary = "Agregar Imágenes", Description = "Endpoint para agregar múltiples imágenes.")]
        //public async Task<IActionResult> Add([FromBody] List<AddImageDTO> createList)
        //{
        //    try
        //    {
        //        var errors = new List<string>();
        //        var validImages = new List<AddImageDTO>();
        //        var successfullyCreated = new List<int?>();

        //        foreach (var create in createList)
        //        {
        //            var validationResult = await _validatorCreate.ValidateAsync(create);
        //            if (validationResult.IsValid)
        //            {
        //                validImages.Add(create);
        //            }
        //            else
        //            {
        //                errors.AddRange(validationResult.Errors.Select(e => $"Error en una imagen: {e.ErrorMessage}"));
        //            }
        //        }

        //        if (errors.Any())
        //        {
        //            return BadRequest(Response<string>.BadRequest(errors, 400));
        //        }

        //        foreach (var image in validImages)
        //        {
        //            var created = await _imagenesService.Add(image);
        //            successfullyCreated.Add(created);
        //        }

        //        return Ok(Response<List<int?>>.Created(successfullyCreated));
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details (if logging is configured)
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            Response<string>.ServerError($"Ocurrió un error al procesar la solicitud: {ex.Message}"));
        //    }
        //}


    }
}
