using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaGuest : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Names { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!; 
        public string? NickName { get; set; }
        public long CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual GnEmpresa? Company { get; set; }
    }
}
