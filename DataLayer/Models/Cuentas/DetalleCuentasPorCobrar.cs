using DataLayer.Models.Otros;
using System;

namespace DataLayer.Models.Cuentas
{
    public class DetalleCuentasPorCobrar : BaseModel
    {
        public int FacturacionId { get; set; }
              
        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }

        public bool IsCalceled { get; set; }

        public int IdUsuario { get; set; }


    }
}
