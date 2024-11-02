using System;

namespace BussinessLayer.DTOs.Seguridad
{
    public class GnPerfilResponse
    {
        public int IDPerfil { get; set; }
        public string Perfil { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long IDEmpresa { get; set; }
    }
}
