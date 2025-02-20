using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class Usuario : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public long? CodigoEmp { get; set; }
        public int? IdHorario { get; set; }
        public int? IdPerfil { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? ImagenUsuario { get; set; }
        public string? TelefonoPersonal { get; set; }
        public string? Telefono { get; set; }
        public bool OnlineUsuario { get; set; }
        public long? CodigoSuc { get; set; }
        public string? IpAdiccion { get; set; }
        public string? IpModificacion { get; set; }
        public string Longitud { get; set; } = "0";
        public string Latitud { get; set; } = "0";

        public bool IsActive = true;
        public string Email { get; set; } = null!;
        public string? LanguageCode { get; set; } 
        public string? DefaultUrl { get; set; } 
        public string? UserName { get; set; }
        [ForeignKey("CodigoSuc")]
        public GnSucursal? GnSucursal { get; set; }
        [ForeignKey("CodigoEmp")]
        public GnEmpresa? GnEmpresa { get; set; }
        [ForeignKey("IdPerfil")]
        public GnPerfil? GnPerfil { get; set; }
    }
}
