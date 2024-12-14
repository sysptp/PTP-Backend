using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloVentas.Caja
{
    public class Billetes_Moneda
    {
        [Key]
        public int id { get; set; }
        public string descripcion { get; set; }
        public int idMoneda { get; set; }
        public int numeroOrden { get; set; }
        public long idEmpresa { get; set; }

    }
}
