using System;

namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil
{
    public class GnPerfilResponse
    {
        public int IdRole { get; set; }
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long CompanyId { get; set; }
    }
}
