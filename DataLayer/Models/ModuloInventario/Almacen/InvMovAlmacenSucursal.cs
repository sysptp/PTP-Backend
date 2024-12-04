using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursal: AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdTipoMovimiento { get; set; }
        public int CantidadProducto { get; set; }
        public string Motivo { get; set; }
        public bool Activo { get; set; }
       
    }
}
