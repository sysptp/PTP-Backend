using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvInventarioSucursal: AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string UbicacionExhibicion { get; set; }
        public string UbicacionGuardada { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadMinima { get; set; }
        public bool Activo { get; set; }
       
    }
}
