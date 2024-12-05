using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaEmailConfiguracion : AuditableEntities
    {
        [Key]
        public int IdEmailConfiguration { get; set; }
        public int IdUser { get; set; }
        public bool IsMailbox { get; set; } = false;
        public string Email { get; set; } = null!;
    }
}
