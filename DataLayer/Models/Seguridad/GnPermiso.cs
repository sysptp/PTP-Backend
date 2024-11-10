using DataLayer.Models.Otros;

namespace DataLayer.Models.Seguridad
{
    public class GnPermiso : AuditableEntities
    {
        public long IDPermiso { get; set; }
        public int IDPerfil { get; set; }
        public int IDMenu { get; set; }
        public long Codigo_EMP { get; set; }
        public bool Crear { get; set; }
        public bool Eliminar { get; set; }
        public bool Editar { get; set; }
        public bool Consultar { get; set; }
    }
}
