
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

[ApiController]
[SwaggerTag("Gestión de asignación de usuarios a áreas")]
[Route("api/v1/[controller]")]
[Authorize]
public class CtaAreaXUserController : ControllerBase
{
    private readonly ICtaAreaXUserService _areaXUserService;
    private readonly IValidator<CtaAreaXUserRequest> _validator;

    public CtaAreaXUserController(
        ICtaAreaXUserService areaXUserService,
        IValidator<CtaAreaXUserRequest> validator)
    {
        _areaXUserService = areaXUserService;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<IEnumerable<CtaAreaXUserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Obtener asignaciones de usuarios a áreas", Description = "Devuelve una lista de asignaciones o una asignación específica si se proporciona un ID")]
    public async Task<IActionResult> GetAllAssignments([FromQuery] int? id)
    {
        try
        {
            if (id.HasValue)
            {
                var assignment = await _areaXUserService.GetByIdResponse(id.Value);
                if (assignment == null)
                    return NotFound(Response<CtaAreaXUserResponse>.NotFound("Asignación no encontrada."));

                return Ok(Response<CtaAreaXUserResponse>.Success(assignment, "Asignación encontrada."));
            }

            var assignments = await _areaXUserService.GetAllDto();
            if (assignments == null || !assignments.Any())
                return StatusCode(204, Response<IEnumerable<CtaAreaXUserResponse>>.NoContent("No hay asignaciones disponibles."));

            return Ok(Response<IEnumerable<CtaAreaXUserResponse>>.Success(assignments, "Asignaciones obtenidas correctamente."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.ServerError(ex.Message));
        }
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Crear una nueva asignación", Description = "Endpoint para registrar una nueva asignación de usuario a área")]
    public async Task<IActionResult> CreateAssignment([FromBody] CtaAreaXUserRequest assignmentRequest)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(assignmentRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var response = await _areaXUserService.Add(assignmentRequest);
            return CreatedAtAction(nameof(GetAllAssignments), Response<CtaAreaXUserResponse>.Created(response));
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
    [SwaggerOperation(Summary = "Actualizar una asignación", Description = "Endpoint para actualizar una asignación de usuario a área")]
    public async Task<IActionResult> UpdateAssignment(int id, [FromBody] CtaAreaXUserRequest assignmentRequest)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(assignmentRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            var existingAssignment = await _areaXUserService.GetByIdRequest(id);
            if (existingAssignment == null)
                return NotFound(Response<string>.NotFound("Asignación no encontrada."));

            assignmentRequest.AreaXUserId = id;
            await _areaXUserService.Update(assignmentRequest, id);
            return Ok(Response<string>.Success(null, "Asignación actualizada correctamente."));
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
    [SwaggerOperation(Summary = "Eliminar una asignación", Description = "Endpoint para eliminar una asignación de usuario a área")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        try
        {
            var existingAssignment = await _areaXUserService.GetByIdRequest(id);
            if (existingAssignment == null)
                return NotFound(Response<string>.NotFound("Asignación no encontrada."));

            await _areaXUserService.Delete(id);
            return Ok(Response<string>.Success(null, "Asignación eliminada correctamente."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.ServerError(ex.Message));
        }
    }

}
