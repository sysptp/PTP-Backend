using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones
{
    public class CmpConfiguracionCreateDto
    {
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int ServidorId { get; set; }
        public long EmpresaId { get; set; }
        public string? UsuarioAdicion { get; set; }
    }
}
