using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Versiones
{
    public class CreateVersionsDto
    {
        public string? Nombre { get; set; }

        public int IdMarca { get; set; }

        public long IdEmpresa { get; set; }
    }
}
