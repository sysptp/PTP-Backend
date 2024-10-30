using Microsoft.AspNetCore.Identity;

namespace IdentityLayer.Entities
{
    public class SC_USUAR001 : IdentityUser<long> 
    {
        public long? CodigoEmp { get; set; } 
        public string NombreUsuario { get; set; } 
        public string Password { get; set; }
        public int? IdHorario { get; set; }
        public int? IdPerfil { get; set; }
        public bool CorreoConfirmado { get; set; }
        public string ImagenUsuario { get; set; }
        public string Correo { get; set; }
        public string TelefonoPersonal { get; set; }
        public string ExtencionPersonal { get; set; }
        public string Telefono { get; set; }
        public string Extencion { get; set; }
        public bool OnlineUsuario { get; set; }
        public long? CodigoSuc { get; set; }
        public string IpAdiccion { get; set; }
        public string IpModificacion { get; set; }
        public int UsuarioAdiccion { get; set; }
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public int? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Longitud { get; set; } = "0";
        public string Latitud { get; set; } = "0";
    }
}
