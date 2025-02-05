using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloGeneral.SMTP
{
    public class GnSmtpConfiguracion : AuditableEntities
    {
        public int IdSmtp { get; set; }
        public string Servidor { get; set; } = null!;
        public int Puerto { get; set; }
        public string UsuarioSmtp { get; set; } = null!;
        public string PassUsuario { get; set; } = null!;
        public string? Remitente { get; set; } 
        public bool EsUsuario { get; set; }
        public string? NombreRemitente { get; set; }
        public long IdEmpresa { get; set; }
    }
}
