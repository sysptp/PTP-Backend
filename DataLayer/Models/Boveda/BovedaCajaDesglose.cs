using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Boveda
{
    public class BovedaCajaDesglose
    {
     [Key]
        public int id { get; set; }
        public int idBoveda { get; set; }
        public int idIdmoneda { get; set; }
        public string idbilletes_moneda { get; set; }
        public int Cantidad { get; set; }
        public long idEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
