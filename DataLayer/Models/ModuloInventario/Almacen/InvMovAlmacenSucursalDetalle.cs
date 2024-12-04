using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalDetalle:AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdMovAlmacenSucursal { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
        
    }
}
