using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class Gn_Permiso
    {

        public int IDPerfil { get; set; }
        public int IDMenu { get; set; }
        [NotMapped]
        public bool Check { get; set; }
        [Key]
        public long IDPerminso { get; set; }


        public long? IDEmpresa { get; set; }

    }
}
