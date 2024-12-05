using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkDepartaments : AuditableEntities
    {
        [Key]
        public int IdDepartamentos { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public bool EsPrincipal { get; set; }

    }
}
