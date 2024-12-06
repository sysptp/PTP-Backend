using BussinessLayer.DTOs.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovimientoSucursalDetalleReponse:AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdMovInventarioSucursal { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
