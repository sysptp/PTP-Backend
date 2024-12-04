using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovInventarioSucursal : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdModulo { get; set; }
        public int IdTransaccionOrigen { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
     
    }
}
