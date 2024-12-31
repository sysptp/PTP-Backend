using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaEmailConfiguracion : AuditableEntities
    {
        [Key]
        public int IdEmailConfiguration { get; set; }
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public Usuario? Usuario { get; set; }
        public bool IsMailbox { get; set; } = false;
        public string Email { get; set; } = null!;
    }
}
