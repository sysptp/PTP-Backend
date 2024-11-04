using Microsoft.AspNetCore.Mvc;
using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.Interfaces.IMenu;
using BussinessLayer.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.Configuration
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Gestión de Menús")]
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
        public async Task<IActionResult> GetMenuHierarchy([FromQuery] int? roleId, [FromQuery] long? companyId,bool isHierarchy)
        {
            try
            {
                var menus = await _menuService.GetMenuHierarchy(roleId, companyId,isHierarchy);

                if (menus == null || !menus.Any())
                {
                    return StatusCode(204, Response<IEnumerable<GnMenuResponse>>.NoContent("No hay menús disponibles para este rol y empresa."));
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
                return Ok(Response<string>.Success("Menú actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el menú. Por favor, intente nuevamente."));
            }
        }


    }
}
