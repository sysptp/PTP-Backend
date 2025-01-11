using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkErrorSubCategory : AuditableEntities
    {
        [Key]
        public int IdErroSubCategory { get; set; }
        public int IdSubCategory { get; set; }
        [ForeignKey("IdSubCategory")]
        public HdkSubCategory? HdkSubCategory { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }

    }
}
