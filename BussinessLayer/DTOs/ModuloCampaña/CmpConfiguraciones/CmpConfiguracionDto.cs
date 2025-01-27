using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones
{
    public class CmpConfiguracionDto
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int ServidorId { get; set; }
        public long EmpresaId { get; set; }
        public CmpServidoresSmtpDto ServidoresSmtp { get; set; }

    }
}
