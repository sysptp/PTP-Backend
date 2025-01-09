using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovInventarioSucursal : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public long IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public GnSucursal? GnSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdModulo { get; set; }
        public int IdTransaccionOrigen { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
     
    }
}
