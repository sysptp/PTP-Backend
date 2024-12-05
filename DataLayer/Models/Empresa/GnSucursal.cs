
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Empresa
{
    public class GnSucursal : AuditableEntities
    {
        [Key]
        public long CodigoSuc { get; set; }

        public long CodigoEmp { get; set; }

        public string NombreSuc { get; set; } = null!;

        public string? Telefono1 { get; set; }

        public int? IdUsuarioResponsable { get; set; }

        public int CodPais { get; set; }

        public int CodRegion { get; set; }

        public int CodProvincia { get; set; }

        public int IdMunicipio { get; set; }

        public string? Direccion { get; set; }

        public bool EstadoSuc { get; set; }

        public string? IpAdicion { get; set; }

        public string? IpModificacion { get; set; }

        public string? Longitud { get; set; }

        public string? Latitud { get; set; }

        public bool Principal { get; set; }

    }
}
