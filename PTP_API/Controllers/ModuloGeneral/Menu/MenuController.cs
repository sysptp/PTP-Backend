using Microsoft.AspNetCore.Mvc;

using BussinessLayer.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.ModuloGeneral.Menu;
using BussinessLayer.DTOs.ModuloGeneral.Menu;

namespace PTP_API.Controllers.ModuloGeneral.Menu
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Gestión de Menús")]
    [EnableBitacora]
    public class MenuController : ControllerBase
    {
        private readonly IGnMenuService _menuService;

        public MenuController(IGnMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("hierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener jerarquía de menús", Description = "Obtiene una jerarquía de menús basada en el rol de usuario y la empresa proporcionados.")]
        [DisableBitacora]
        public async Task<IActionResult> GetMenuHierarchy([FromQuery] int? roleId, [FromQuery] long? companyId, bool isHierarchy)
        {
            try
            {
                var menus = await _menuService.GetMenuHierarchy(roleId, companyId, isHierarchy);

                if (menus == null || !menus.Any())
                {
                    return NoContent();
                }

                return Ok(Response<IEnumerable<GnMenuResponse>>.Success(menus, "Menús obtenidos correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener la jerarquía de menús. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar menú", Description = "Actualiza la información de un menú existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] SaveGnMenuRequest menuRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingMenu = await _menuService.GetByIdRequest(id);
                if (existingMenu == null)
                {
                    return NotFound(Response<string>.NotFound("Menú no encontrado."));
                }

                menuRequest.IDMenu = id;
                await _menuService.Update(menuRequest, id);
                return Ok(Response<string>.Success(null, "Menú actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el menú. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo menú", Description = "Endpoint para crear un nuevo menú.")]
        public async Task<IActionResult> Add([FromBody] SaveGnMenuRequest menuRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var createdMenu = await _menuService.Add(menuRequest);
                return CreatedAtAction(nameof(GetMenuHierarchy), createdMenu, Response<object>.Success(createdMenu, "Menú creado exitosamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el menú. Por favor, intente más tarde."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar menú", Description = "Endpoint para eliminar un menú.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingMenu = await _menuService.GetByIdRequest(id);
                if (existingMenu == null)
                {
                    return NotFound(Response<string>.NotFound("Menú no encontrado."));
                }

                await _menuService.Delete(id);
                return Ok(Response<string>.Success(null, "Menú eliminado satisfactoriamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el menú. Por favor, intente más tarde."));
            }
        }

    }
}
