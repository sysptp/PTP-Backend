using DataLayer.Models.ModuloInventario;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.ModuloInventario.Precios
{
    public class CreatePreciosDto
    {
        public int? Id { get; set; }

        public int? IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        public decimal PrecioValor { get; set; }

    }
}
