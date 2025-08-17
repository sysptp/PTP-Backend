using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.ModuloGeneral.Empresas
{
    public class CompanyRegistrationRequest
    {
        [SwaggerSchema("Company information")]
        [Required(ErrorMessage = "Company information is required")]
        public GnEmpresaRequest Company { get; set; } = null!;

        [SwaggerSchema("Admin user information")]
        [Required(ErrorMessage = "Admin user information is required")]
        public AdminUserRegistrationRequest AdminUser { get; set; } = null!;

        [SwaggerSchema("List of module IDs to assign to the company")]
        [Required(ErrorMessage = "At least one module must be selected")]
        [MinLength(1, ErrorMessage = "At least one module must be selected")]
        public List<int> SelectedModuleIds { get; set; } = new List<int>();
    }
}