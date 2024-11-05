using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario
{
    public class CreateMarcasDto
    {
        public long? IdEmpresa { get; set; }

        public string? Nombre { get; set; }

    }
}
