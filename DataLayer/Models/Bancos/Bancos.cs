using DataLayer.Models.Empresa;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Bancos
{
    public class Bancos
    {
        [Key]
        public int Id { get; set; }
        public string Banco { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int idUsuarioCrea { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual SC_EMP001 empresa { get; set; }
        public long IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public virtual SC_SUC001 sucursal { get; set; }
    }
}
