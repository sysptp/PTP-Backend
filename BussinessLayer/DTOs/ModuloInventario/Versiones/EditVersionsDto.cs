using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Versiones
{
    public class EditVersionsDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public bool? Activo { get; set; }

        public int? IdMarca { get; set; }

    }
}
