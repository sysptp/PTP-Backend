using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloVentas.Boveda
{
    public class BovedaCaja
    {
        [Key]
        public int id { get; set; }
        public string Descripcion { get; set; }
        public int idMoneda { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioIDCrea { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int UsuarioIDActualiza { get; set; }
        public long idEmpresa { get; set; }
        public long IdSucursal { get; set; }
        public int idUsuarioResposable { get; set; }
        public int idUsuarioModifica { get; set; }
    }
}
