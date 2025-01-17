
namespace BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas
{
    public class CmpTipoPlantillaUpdateDto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int EmpresaId { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
