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
        public async Task<IActionResult> GetMenuHierarchy([FromQuery] int? roleId, [FromQuery] long? companyId)
        {
            try
            {
                var menus = await _menuService.GetMenuHierarchy(roleId, companyId);

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
    }
}
