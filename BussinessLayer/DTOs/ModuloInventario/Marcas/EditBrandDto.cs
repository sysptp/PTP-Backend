using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Marcas
{
    public class EditBrandDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public bool? Activo { get; set; }
    }
}
