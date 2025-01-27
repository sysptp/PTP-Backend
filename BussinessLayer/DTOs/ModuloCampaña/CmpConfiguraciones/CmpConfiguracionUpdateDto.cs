namespace BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones
{
    public class CmpConfiguracionUpdateDto
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int ServidorId { get; set; }
        public long EmpresaId { get; set; }
        public string UsuarioModificacion {  get; set; }
    }
}
