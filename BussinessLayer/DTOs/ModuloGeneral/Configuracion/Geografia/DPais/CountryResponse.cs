using BussinessLayer.Atributes;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais
{

    [TableName("Pais")]
    public class CountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
