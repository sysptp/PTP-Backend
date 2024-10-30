namespace BussinessLayer.DTOs.Seguridad
{
    public class GnPerfilDto
    {
        public int IDPerfil { get; set; }
        public string Perfil { get; set; }
        public string? Descripcion { get; set; }
        public long? IDEmpresa { get; set; }
        public DateTime FechaCreada { get; set; } = DateTime.Now;
        public DateTime? UltimaFechaModificacion { get; set; }
    }
}

