using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Suplidores
{
    public class ViewContactosSuplidoresDto
    {
        public int Id { get; set; }
        public int IdSuplidor { get; set; }
        public long? IdEmpresa { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Extension { get; set; }
    }
}
